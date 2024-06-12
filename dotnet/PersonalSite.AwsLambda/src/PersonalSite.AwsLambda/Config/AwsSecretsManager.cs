using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace PersonalSite.Web.Config;

/// <summary>
/// https://aws.amazon.com/blogs/modernizing-with-aws/how-to-load-net-configuration-from-aws-secrets-manager/
/// https://docs.aws.amazon.com/secretsmanager/latest/userguide/retrieving-secrets_lambda.html
/// </summary>
public class AwsSecretsManagerConfigurationProvider : ConfigurationProvider
{
    private readonly string _region;
    private readonly string _secretName;
    
    public AwsSecretsManagerConfigurationProvider(string region, string secretName)
    {
        _region = region;
        _secretName = secretName;
    }

    public override void Load()
    {
        var secret = GetSecret();
        Data = JsonSerializer.Deserialize<Dictionary<string, string>>(secret)!;
    }

    private string GetSecret()
    {
        var secretsToken = Environment.GetEnvironmentVariable("AWS_SECRETS_MANAGER_TOKEN");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Aws-Parameters-Secrets-Token", secretsToken);
        var request = new HttpRequestMessage(HttpMethod.Get, 
            $"http://localhost:2773/secretsmanager/get?secretId={_secretName}");
        var response = client.Send(request);

        var responseContent = response.Content.ReadFromJsonAsync<SecretsResponse>().GetAwaiter().GetResult();
        
        string secretString;
        if (responseContent?.SecretString != null)
        {
            secretString = responseContent.SecretString;
        }
        else
        {
            var memoryStream = responseContent?.SecretBinary ?? throw new InvalidOperationException("Secret binary is null");
            var reader = new StreamReader(memoryStream);
            secretString = 
                System.Text.Encoding.UTF8
                    .GetString(Convert.FromBase64String(reader.ReadToEnd()));
        }

        return secretString;
    }

    private class SecretsResponse
    {
        public string? SecretString { get; set; }
        public MemoryStream? SecretBinary { get; set; }
    }
}

public class AwsSecretsManagerConfigurationSource : IConfigurationSource
{
    private readonly string _region;
    private readonly string _secretName;

    public AwsSecretsManagerConfigurationSource(string region, string secretName)
    {
        _region = region;
        _secretName = secretName;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new AwsSecretsManagerConfigurationProvider(_region, _secretName);
    }
}

public static class Extensions
{
    public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder, 
        string? region,
        string? secretName)
    {
        if (string.IsNullOrEmpty(region) || string.IsNullOrEmpty(secretName))
            throw new ArgumentException($"Region name and secret name are required");
        var configurationSource = 
            new AwsSecretsManagerConfigurationSource(region, secretName);

        configurationBuilder.Add(configurationSource);
    }
}