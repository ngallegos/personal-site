using Contentful.Core;
using Contentful.Core.Search;

namespace PersonalSite.ContentModel;

public interface IContentService
{
    Task<SiteMetaData?> GetSiteMetaDataAsync(string domain);
    Task<Page?> GetPageAsync(string domain, string slug);
    Task<Resume?> GetResumeAsync(string domain);
}

public class ContentService(IContentfulClient contentful) : IContentService
{
    public virtual async Task<SiteMetaData?> GetSiteMetaDataAsync(string domain)
    {
        var query = QueryBuilder<SiteMetaData>.New.FieldMatches(x => x.Domains, domain);
        return (await contentful.GetEntriesByType<SiteMetaData>(query)).FirstOrDefault();
    }
    
    public virtual async Task<Page?> GetPageAsync(string domain, string slug)
    {
        var query = QueryBuilder<Page>.New.FieldEquals(x => x.Slug, slug)
            .FieldMatches(x => x.Domains, domain);
        return (await contentful.GetEntriesByType<Page>(query)).FirstOrDefault();
    }
    
    public virtual async Task<Resume?> GetResumeAsync(string domain)
    {
        var query = QueryBuilder<Resume>.New.FieldEquals(x => x.Active, "true")
            .FieldMatches(x => x.Domains, domain);
        return (await contentful.GetEntriesByType<Resume>(query)).MaxBy(x => x.Sys?.UpdatedAt ?? x.Sys?.CreatedAt);
    }
}