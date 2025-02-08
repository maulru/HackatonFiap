using AgendaAPI.Infrastructure.Services.Email;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;


public class RabbitMQEmailQueuePublisher : IEmailQueuePublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;
    private readonly string _queueName = "emailQueue";

    public RabbitMQEmailQueuePublisher(IConfiguration configuration)
    {
        var factory = new ConnectionFactory()
        {
            // As configurações podem vir do appsettings.json ou variáveis de ambiente
            HostName = configuration["RabbitMQ:HostName"] ?? "rabbitmq",
            UserName = configuration["RabbitMQ:UserName"] ?? "guest",
            Password = configuration["RabbitMQ:Password"] ?? "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declara a fila (durável, não exclusiva, persistente)
        _channel.QueueDeclare(queue: _queueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void PublishEmail(EmailQueueItem emailItem)
    {
        var json = JsonSerializer.Serialize(emailItem);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(exchange: "",
                              routingKey: _queueName,
                              basicProperties: null,
                              body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
