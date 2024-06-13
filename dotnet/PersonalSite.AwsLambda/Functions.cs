using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Contentful.Core;
using PersonalSite.AwsLambda.Extensions;
using PersonalSite.ContentModel;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PersonalSite.AwsLambda;

public class Functions
{
    private IContentServiceFactory _contentServiceFactory;

    public Functions(IContentServiceFactory contentServiceFactory)
    {
        _contentServiceFactory = contentServiceFactory;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/{domain}/{contentType}/{slug}")]
    public async Task<IHttpResult> GetContent(ILambdaContext context, string domain, string contentType,  string slug = "")
    {
        slug = "/" + (slug ?? "").TrimStart('/');
        context.Logger.LogInformation($"GET ${contentType} for domain: {domain}, slug: {slug}");
        return contentType.ToLower() switch
        {
            "resume" => await GetResume(domain, context),
            "meta" => await GetSiteMetaData(domain, context),
            "page" => await GetPage(domain, slug, context),
            _ => throw new ArgumentException($"Invalid content type: {contentType}")
        };
    }

    private async Task<IHttpResult> GetResume(string domain, ILambdaContext context)
    {
        var contentService = await _contentServiceFactory.GetContentService();
        var resume = await contentService.GetResumeAsync();
        
        return HttpResults.Ok(JsonSerializer.Serialize(resume, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase }))
            .AddHeader("Access-Control-Allow-Origin", "*")
            .AddHeader("Content-Type", "application/json");
    }

    private async Task<IHttpResult> GetSiteMetaData(string domain, ILambdaContext context)
    {
        var contentService = await _contentServiceFactory.GetContentService();
        var meta = await contentService.GetSiteMetaDataAsync(domain);
        return HttpResults.Ok(JsonSerializer.Serialize(meta, new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase }))
            .AddHeader("Access-Control-Allow-Origin", "*")
            .AddHeader("Content-Type", "application/json");
    }

    private async Task<IHttpResult> GetPage(string domain, string slug, ILambdaContext context)
    {
        var contentService = await _contentServiceFactory.GetContentService();
        var page = await contentService.GetPageAsync(slug);
        return HttpResults.Ok(page?.Content ?? "")
            .AddHeader("Access-Control-Allow-Origin", "*");
    }
}