using Contentful.Core;
using Microsoft.Extensions.Caching.Memory;
using PersonalSite.ContentModel;

public class CachedContentService(
    IContentfulClient contentful,
    IMemoryCache cache,
    IConfiguration configuration)
    : ContentService(contentful)
{
    private readonly IContentfulClient _contentful = contentful;
    private readonly int _cacheDurationSeconds = configuration.GetValue<int>("Cache:ContentfulCacheSeconds");

    public override async Task<SiteMetaData?> GetSiteMetaDataAsync(string domain)
    {
        return await cache.GetOrCreateAsync($"siteMetaData:{domain}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetSiteMetaDataAsync(domain);
        });
    }
    
    public override async Task<Page?> GetPageAsync(string domain, string slug)
    {
        return await cache.GetOrCreateAsync($"page:{slug}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetPageAsync(domain, slug);
        });
    }
    
    public override async Task<Resume?> GetResumeAsync(string domain)
    {
        return await cache.GetOrCreateAsync("resume", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetResumeAsync(domain);
        });
    }
}