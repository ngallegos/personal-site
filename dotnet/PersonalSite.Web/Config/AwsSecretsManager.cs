using System.Diagnostics;
using System.Text.Json;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace PersonalSite.Web.Config;

/// <summary>
/// https://aws.amazon.com/blogs/modernizing-with-aws/how-to-load-net-configuration-from-aws-secrets-manager/
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
        var request = new GetSecretValueRequest
        {
            SecretId = _secretName,
            VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
        };

        using var client = new AmazonSecretsManagerClient(GetCredentials("personal-site"), RegionEndpoint.GetBySystemName(_region));
        var response = client.GetSecretValueAsync(request).Result;

        string secretString;
        if (response.SecretString != null)
        {
            secretString = response.SecretString;
        }
        else
        {
            var memoryStream = response.SecretBinary;
            var reader = new StreamReader(memoryStream);
            secretString = 
                System.Text.Encoding.UTF8
                    .GetString(Convert.FromBase64String(reader.ReadToEnd()));
        }

        return secretString;
    }
    
    private static AWSCredentials GetCredentials(string profileName = "default")
    {
        if (!Debugger.IsAttached)
            return new GenericContainerCredentials();
        
        // use the aws cli or environment variables for local development
        var chain = new CredentialProfileStoreChain();
        if (chain.TryGetAWSCredentials(profileName, out var creds))
            return creds;
        return new EnvironmentVariablesAWSCredentials();
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