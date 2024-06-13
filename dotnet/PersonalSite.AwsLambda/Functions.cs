using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Contentful.Core;
using PersonalSite.AwsLambda.Extensions;
using PersonalSite.ContentModel;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PersonalSite.AwsLambda;

/// <summary>
/// A collection of sample Lambda functions that provide a REST api for doing simple math calculations. 
/// </summary>
public class Functions
{
    private IContentServiceFactory _contentServiceFactory;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <remarks>
    /// The <see cref="ICalculatorService"/> implementation that we
    /// instantiated in <see cref="Startup"/> will be injected here.
    /// 
    /// As an alternative, a dependency could be injected into each 
    /// Lambda function handler via the [FromServices] attribute.
    /// </remarks>
    public Functions(IContentServiceFactory contentServiceFactory)
    {
        _contentServiceFactory = contentServiceFactory;
    }

    [LambdaFunction()]
    [RestApi(LambdaHttpMethod.Get, "/{domain}/resume")]
    public async Task<IHttpResult> GetResume(string domain, ILambdaContext context)
    {
        var contentService = await _contentServiceFactory.GetContentService();
        var resume = await contentService.GetResumeAsync();
        context.Logger.LogInformation($"GET resume for domain: {domain}");
        
        return HttpResults.Ok(JsonSerializer.Serialize(resume, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase }))
            .AddHeader("Access-Control-Allow-Origin", "*")
            .AddHeader("Content-Type", "application/json");
    }

    [LambdaFunction()]
    [RestApi(LambdaHttpMethod.Get, "/{domain}/meta")]
    public async Task<IHttpResult> GetSiteMetaData(string domain, ILambdaContext context)
    {
        var contentService = await _contentServiceFactory.GetContentService();
        var meta = await contentService.GetSiteMetaDataAsync(domain);
        context.Logger.LogInformation($"GET site meta for domain: {domain}");
        return HttpResults.Ok(JsonSerializer.Serialize(meta, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase }))
            .AddHeader("Access-Control-Allow-Origin", "*");
    }

    [LambdaFunction()]
    [RestApi(LambdaHttpMethod.Get, "/{domain}/page/{slug}")]
    public async Task<IHttpResult> GetPage(string domain, string slug, ILambdaContext context)
    {
        var contentService = await _contentServiceFactory.GetContentService();
        var page = await contentService.GetPageAsync("about");
        context.Logger.LogInformation($"GET page for domain: {domain}, slug: {slug}");
        return HttpResults.Ok(page?.Content ?? "")
            .AddHeader("Access-Control-Allow-Origin", "*")
            .AddHeader("Content-Type", "application/json");
    }
}