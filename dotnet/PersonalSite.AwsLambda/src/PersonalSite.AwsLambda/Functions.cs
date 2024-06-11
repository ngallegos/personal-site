using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using PersonalSite.ContentModel;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PersonalSite.AwsLambda;

/// <summary>
/// A collection of sample Lambda functions that provide a REST api for doing simple math calculations. 
/// </summary>
public class Functions
{
    private IContentService _contentService;

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
    public Functions(IContentService contentService)
    {
        _contentService = contentService;
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/{domain}/resume")]
    public async Task<Resume?> GetResume(string domain, ILambdaContext context)
    {
        var resume = await _contentService.GetResumeAsync();
        context.Logger.LogInformation($"GET resume for domain: {domain}");
        return resume;
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/{domain}/meta")]
    public async Task<SiteMetaData?> GetSiteMetaData(string domain, ILambdaContext context)
    {
        var meta = await _contentService.GetSiteMetaDataAsync(domain);
        context.Logger.LogInformation($"GET site meta for domain: {domain}");
        return meta;
    }

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, "/{domain}/page/{slug}")]
    public async Task<string?> GetPage(string domain, string slug, ILambdaContext context)
    {
        var page = await _contentService.GetPageAsync("about");
        context.Logger.LogInformation($"GET page for domain: {domain}, slug: {slug}");
        return page?.Content ?? "";
    }
}