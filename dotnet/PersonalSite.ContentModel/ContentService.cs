using Contentful.Core;
using Contentful.Core.Search;
using PersonalSite.ContentModel.Blog;

namespace PersonalSite.ContentModel;

public interface IContentService
{
    Task <Post?> GetBlogPostAsync(string domain, string slug);
    Task <List<Post>> GetBlogPostsAsync(string domain, string? tag = null, int skip = 0, int limit = 10);
    Task<SiteMetaData?> GetSiteMetaDataAsync(string domain);
    Task<Page?> GetPageAsync(string domain, string slug);
    Task<Resume?> GetResumeAsync(string domain);
}

public class ContentService(IContentfulClient contentful) : IContentService
{
    public virtual async Task<Post?> GetBlogPostAsync(string domain, string slug)
    {
        var query = QueryBuilder<Post>.New.FieldEquals(x => x.Slug, slug)
            .FieldMatches(x => x.Domains, domain);
        return (await contentful.GetEntriesByType<Post>(query)).FirstOrDefault();
    }

    public virtual async Task<List<Post>> GetBlogPostsAsync(string domain, string? tag = null, int skip = 0, int limit = 10)
    {
        var query = QueryBuilder<Post>.New.FieldMatches(x => x.Domains, domain);
        var sort = SortOrderBuilder<Post>.New(x => x.Sticky, SortOrder.Reversed)
            .ThenBy(x => x.Sys!.UpdatedAt, SortOrder.Reversed)
            .ThenBy(x => x.Sys!.CreatedAt, SortOrder.Reversed)
            .Build();
        if (!string.IsNullOrEmpty(tag))
        {
            var tagQuery = QueryBuilder<Tag>.New.FieldEquals(x => x.Name, tag).Include(0).Limit(1);
            var tagEntry = (await contentful.GetEntriesByType<Tag>(tagQuery)).FirstOrDefault();
            if (tagEntry != null)
            {
                query = query.LinksToEntry(tagEntry.Sys?.Id);   
            }
        }
        query = query.OrderBy(sort).Skip(skip).Limit(limit);

        var posts = await contentful.GetEntriesByType<Post>(query);
        return posts.ToList();
    }

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