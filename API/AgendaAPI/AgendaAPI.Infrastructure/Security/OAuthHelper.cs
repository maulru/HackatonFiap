using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

public class OAuthHelper
{
    private static readonly string ClientId = "0f6c4332-f12e-4316-886e-10d75fc17e26";
    private static readonly string TenantId = "consumers"; // 🔹 Para contas pessoais, use "consumers"
    private static readonly string[] Scopes = { "https://graph.microsoft.com/.default" }; // 🔹 Escopo correto

    public static async Task<string> GetAccessTokenAsync()
    {
        var app = PublicClientApplicationBuilder
            .Create(ClientId)
            .WithAuthority($"https://login.microsoftonline.com/{TenantId}") // 🔹 Especificando o tenant corretamente
            .WithRedirectUri("http://localhost") // Apenas um placeholder
            .Build();

        AuthenticationResult result;

        try
        {
            var accounts = await app.GetAccountsAsync();
            result = await app.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync();
        }
        catch (MsalUiRequiredException)
        {
            Console.WriteLine("⚠ Nenhuma sessão anterior encontrada. Iniciando autenticação via Device Code...");

            result = await app.AcquireTokenWithDeviceCode(Scopes, deviceCodeResult =>
            {
                Console.WriteLine("\n🔹 Para autenticar, siga estas etapas:");
                Console.WriteLine($"1️⃣ Acesse: {deviceCodeResult.VerificationUrl}");
                Console.WriteLine($"2️⃣ Insira o código: {deviceCodeResult.UserCode}\n");

                return Task.CompletedTask;
            }).ExecuteAsync();
        }

        Console.WriteLine("✅ Autenticação concluída com sucesso!");
        Console.WriteLine($"🔹 Token OAuth obtido: {result.AccessToken}");

        return result.AccessToken;
    }
}
