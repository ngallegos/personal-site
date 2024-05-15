using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.Extensions.Caching.Memory;
using PersonalSite.ContentModel;

public interface IContentService
{
    Task<SiteMetaData> GetSiteMetaDataAsync(string domain);
    Task<Page> GetPageAsync(string slug);
    Task<Resume> GetResumeAsync();
}

public class ContentService : IContentService
{
    private readonly IContentfulClient _contentful;
    private readonly IMemoryCache _cache;
    private readonly int _cacheDurationSeconds;
    
    public ContentService(IContentfulClient contentful, 
        IMemoryCache cache,
        IConfiguration configuration)
    {
        _contentful = contentful;
        _cache = cache;
        _cacheDurationSeconds = configuration.GetValue<int>("Cache:ContentfulCacheSeconds");
    }
    
    public async Task<SiteMetaData> GetSiteMetaDataAsync(string domain)
    {
        return await _cache.GetOrCreateAsync($"siteMetaData:{domain}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            var query = QueryBuilder<SiteMetaData>.New.FieldMatches(x => x.Domains, domain);
            return (await _contentful.GetEntriesByType<SiteMetaData>(query)).Single();
        });
    }
    
    public async Task<Page> GetPageAsync(string slug)
    {
        return await _cache.GetOrCreateAsync($"page:{slug}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            var query = QueryBuilder<Page>.New.FieldEquals(x => x.Slug, slug);
            return (await _contentful.GetEntriesByType<Page>(query)).Single();
        });
    }
    
    public async Task<Resume> GetResumeAsync()
    {
        return await _cache.GetOrCreateAsync("resume", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_cacheDurationSeconds);
            var query = QueryBuilder<Resume>.New.FieldEquals(x => x.Active, "true");
            return (await _contentful.GetEntriesByType<Resume>(query))
                .OrderByDescending(x => x.Sys?.UpdatedAt ?? x.Sys?.CreatedAt ).Single();
        });
    }
}