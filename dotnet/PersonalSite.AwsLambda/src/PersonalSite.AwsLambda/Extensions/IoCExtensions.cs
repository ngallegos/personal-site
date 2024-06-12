using System.Net.Http.Json;
using System.Text.Json;
using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalSite.ContentModel;

namespace PersonalSite.AwsLambda.Extensions;

public static class IoCExtensions
{
    private static IServiceCollection AddSecretsManagerClient(this IServiceCollection services)
    {
        var secretsToken = Environment.GetEnvironmentVariable("AWS_SESSION_TOKEN");
        services.AddHttpClient<ISecretsManagerClient, SecretsManagerClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:2773/secretsmanager/get");
            client.DefaultRequestHeaders.Add("X-Aws-Parameters-Secrets-Token", secretsToken);
        });

        return services;
    }
    
    public static IServiceCollection AddContentServiceFactory(this IServiceCollection services)
    {
        services.AddSecretsManagerClient();
        services.AddHttpClient(ContentServiceFactory.ContentfulClientName);
        services.AddSingleton<IContentServiceFactory, ContentServiceFactory>();
        return services;
    }
    
}

public interface IContentServiceFactory
{
    Task<IContentService> GetContentService();
}

public class ContentServiceFactory : IContentServiceFactory
{
    public static string ContentfulClientName = "ContentfulClient";
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly ISecretsManagerClient _secretsManagerClient;

    public ContentServiceFactory(IHttpClientFactory clientFactory, 
        IConfiguration configuration, 
        ISecretsManagerClient secretsManagerClient)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _secretsManagerClient = secretsManagerClient;
    }

    public async Task<IContentService> GetContentService()
    {
        var gettingContentfulSecret = _secretsManagerClient.GetSecretAsync(_configuration["AWS:SecretNames:Contentful"]);
        var fullConfigBuilder = new ConfigurationBuilder();
        var contentfulOptionsDictionary = _configuration.GetSection("ContentfulOptions").GetChildren()
            .ToDictionary(x => $"ContentfulOptions:{x.Key}", x => x.Value);
        fullConfigBuilder.AddInMemoryCollection(contentfulOptionsDictionary);
        var contentfulSecretString = await gettingContentfulSecret;
        var contentfulSecrets = JsonSerializer.Deserialize<Dictionary<string, string?>>(contentfulSecretString);
        if (contentfulSecrets != null)
            fullConfigBuilder.AddInMemoryCollection(contentfulSecrets);
        
        var fullConfig = fullConfigBuilder.Build();
        var contentfulOptions = fullConfig.GetSection("ContentfulOptions").Get<ContentfulOptions>();
        var contentfulClient = new ContentfulClient(_clientFactory.CreateClient(ContentfulClientName), contentfulOptions);
        var contentService = new ContentService(contentfulClient);
        return contentService;
    }
}

public interface ISecretsManagerClient
{
    Task<string> GetSecretAsync(string secretName);
}

public class SecretsManagerClient : ISecretsManagerClient
{
    private readonly HttpClient _client;

    public SecretsManagerClient(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<string> GetSecretAsync(string secretName)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"?secretId={secretName}");
        var response = await _client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        //return responseContent;
        try
        {
            var responseContent = JsonSerializer.Deserialize<SecretsResponse>(responseString);
            string secretString;
            if (responseContent == null)
                return "";
            if (responseContent.SecretString != null)
            {
                secretString = responseContent.SecretString;
            }
            else
            {
                var memoryStream = responseContent.SecretBinary ?? new MemoryStream();
                var reader = new StreamReader(memoryStream);
                secretString =
                    System.Text.Encoding.UTF8
                        .GetString(Convert.FromBase64String(await reader.ReadToEndAsync()));
            }

            return secretString;
        } catch (Exception ex)
        {
            throw new Exception($"Bad JSON: {responseString}", ex);
        }
    }
    
    private class SecretsResponse
    {
        public string? SecretString { get; set; }
        public MemoryStream? SecretBinary { get; set; }
    }
}