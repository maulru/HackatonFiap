using AgendaAPI.Infrastructure.Security;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration.UserSecrets;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UsuarioAPI.Domain.Repositories;


public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMedicoRepository _medicoRepository;
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private RabbitMQ.Client.IModel _channel;
    private readonly string _queueName = "emailQueue";

    public Worker(ILogger<Worker> logger, IMedicoRepository medicoRepository, IConfiguration configuration)
    {
        _logger = logger;
        _medicoRepository = medicoRepository;
        _configuration = configuration;
        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:HostName"] ?? "rabbitmq",
            UserName = _configuration["RabbitMQ:UserName"] ?? "guest",
            Password = _configuration["RabbitMQ:Password"] ?? "guest",
            Port = 5672
        };

        Console.WriteLine(factory);

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declara a fila, se ainda n�o existir
        _channel.QueueDeclare(queue: _queueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailItem = JsonSerializer.Deserialize<EmailQueueItem>(message);

                if (emailItem != null)
                {
                    // Se o campo de email estiver vazio, resolva a partir do reposit�rio de m�dicos
                    if (string.IsNullOrEmpty(emailItem.EmailDestino))
                    {
                        emailItem.EmailDestino = await _medicoRepository.GetEmailByMedicoIdAsync(emailItem.IdMedico);
                    }

                    _logger.LogInformation("Processando mensagem para o e-mail: {EmailDestino}", emailItem.EmailDestino);
                    await EnviarEmailAsync(emailItem.EmailDestino, emailItem.DataAgendamento, emailItem.IdAgendamento);
                    _logger.LogInformation("E-mail enviado com sucesso para {EmailDestino}", emailItem.EmailDestino);
                }
                // Confirma o processamento da mensagem
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem da fila.");
                // Em caso de erro, recusa a mensagem e opcionalmente reenvia
                _channel.BasicNack(ea.DeliveryTag, false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: _queueName,
                              autoAck: false,
                              consumer: consumer);

        _logger.LogInformation("Worker iniciado e aguardando mensagens na fila '{QueueName}'", _queueName);

        return Task.CompletedTask;
    }

    /*private async Task EnviarEmailAsync(string emailDestino, DateTime dataAgendamento, int idAgendamento)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Suporte MyMedHealth", "myhealthmed.suporte@outlook.com"));
        message.To.Add(new MailboxAddress("", emailDestino));
        message.Subject = "Confirma��o de Agendamento de Consulta";

        message.Body = new TextPart("plain")
        {
            Text = $"Sua consulta foi agendada para {dataAgendamento:dd/MM/yyyy HH:mm}.\nID do agendamento: {idAgendamento}."
        };

        using var client = new SmtpClient();

        // Obt�m o token de acesso via OAuth 2.0
        string accessToken = await OAuthHelper.GetAccessTokenAsync();
        Console.WriteLine($" Token OAuth usado no SMTP: {accessToken}");

        await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);

        //  Use o SaslMechanismOAuth2 corretamente
        var oauth2 = new SaslMechanismOAuth2("myhealthmed.suporte@outlook.com", accessToken);
        await client.AuthenticateAsync(oauth2);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }*/

    public static async Task EnviarEmailAsync(string emailDestino, DateTime dataAgendamento, int idAgendamento)
    {
        string accessToken = await OAuthHelper.GetAccessTokenAsync();
        Console.WriteLine($"Token OAuth usado na Graph API: {accessToken}");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        var emailContent = new
        {
            message = new
            {
                subject = "Confirma��o de Agendamento de Consulta",
                body = new
                {
                    contentType = "Text",
                    content = $"Sua consulta foi agendada para {dataAgendamento:dd/MM/yyyy HH:mm}.\nID do agendamento: {idAgendamento}."
                },
                toRecipients = new[]
                {
                    new
                    {
                        emailAddress = new
                        {
                            address = emailDestino
                        }
                    }
                },
                from = new
                {
                    emailAddress = new
                    {
                        address = "myhealthmed.suporte@outlook.com",
                        name = "Suporte MyMedHealth"
                    }
                }
            },
            saveToSentItems = "true"
        };

        var jsonEmail = JsonSerializer.Serialize(emailContent);
        var content = new StringContent(jsonEmail, Encoding.UTF8, "application/json");

        // Chamando a API do Microsoft Graph para enviar o e-mail
        var response = await client.PostAsync("https://graph.microsoft.com/v1.0/me/sendMail", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($" Erro ao enviar e-mail: {response.StatusCode} - {errorMessage}");
        }
        else
        {
            Console.WriteLine(" E-mail enviado com sucesso via Microsoft Graph API!");
        }
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
