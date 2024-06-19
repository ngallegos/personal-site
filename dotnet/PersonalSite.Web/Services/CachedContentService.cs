using Contentful.Core;
using Microsoft.Extensions.Caching.Memory;
using PersonalSite.ContentModel;
using PersonalSite.ContentModel.Blog;

public class CachedContentService(
    IContentfulClient contentful,
    IMemoryCache cache,
    IConfiguration configuration)
    : ContentService(contentful)
{
    private readonly IContentfulClient _contentful = contentful;
    private readonly int _cacheDurationSeconds = configuration.GetValue<int>("Cache:ContentfulCacheSeconds");

    public override async Task<Post?> GetBlogPostAsync(string domain, string slug)
    {
        return await cache.GetOrCreateAsync($"blogpost:{domain}/{slug}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetBlogPostAsync(domain, slug);
        });
    }

    public override async Task<List<Post>> GetBlogPostsAsync(string domain, string? tag = null, int skip = 0, int limit = 10)
    {
        return await cache.GetOrCreateAsync($"blogposts:{domain}{tag}{skip}{limit}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetBlogPostsAsync(domain, tag, skip, limit);
        }) ?? new List<Post>();
    }

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
        return await cache.GetOrCreateAsync($"page:{domain}/{slug}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetPageAsync(domain, slug);
        });
    }
    
    public override async Task<Resume?> GetResumeAsync(string domain)
    {
        return await cache.GetOrCreateAsync($"resume:{domain}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            return await base.GetResumeAsync(domain);
        });
    }
}