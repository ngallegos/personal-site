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
    private readonly SortOrderBuilder<Post> _postSortOrder = SortOrderBuilder<Post>
        .New(x => x.Sticky, SortOrder.Reversed)
        .ThenBy(x => x.Sys!.UpdatedAt, SortOrder.Reversed)
        .ThenBy(x => x.Sys!.CreatedAt, SortOrder.Reversed);
    
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

    public virtual async Task<SiteMetaData?> GetPostMetaDataAsync(string domain, Post currentPost, int minRelatedPosts = 5)
    {
        var relatedTagIds = currentPost.Tags?.Select(x => x.Sys?.Id).ToList() ?? new List<string>();
        var sortOrderBuilder = SortOrderBuilder<Post>
            .New(x => x.Sticky, SortOrder.Reversed)
            .ThenBy(x => x.Sys!.UpdatedAt, SortOrder.Reversed)
            .ThenBy(x => x.Sys!.CreatedAt, SortOrder.Reversed);

        throw new NotImplementedException();
    }

    private async Task<List<Post>> GetRelatedPostsAsync(string domain, Post currentPost, int minPosts = 5)
    {
        var posts = new List<Post>();
        var relatedTagIds = currentPost.Tags?.Select(x => x.Sys!.Id).ToList() ?? new List<string>();

        if (relatedTagIds.Any())
        {
            // Helps respect contentful rate limiting
            var relatedTagChunks = relatedTagIds.Chunk(3);

            foreach (var chunk in relatedTagChunks)
            {
                var relatedPostTasks = chunk.Select(tagId =>
                {
                    var builder = QueryBuilder<Post>.New
                        .FieldDoesNotEqual(x => x.Slug, currentPost.Slug)
                        .FieldMatches(x => x.Domains, domain)
                        .LinksToEntry(tagId)
                        .Limit(minPosts)
                        .OrderBy(_postSortOrder.Build());
                    return contentful.GetEntriesByType<Post>(builder);
                });
                
                
                posts.AddRange((await Task.WhenAll(relatedPostTasks)).SelectMany(c => c.Items));
                
                if (posts.Count >= minPosts)
                    break;
            }
            
        }

        if (posts.Count < minPosts)
            posts.AddRange(await GetBlogPostsAsync(domain, null, posts.Count, minPosts - posts.Count));
        
        return posts;
    }

    private async Task<List<Post>> GetBlogPostsAsync(string domain, string tag = null, int minPosts = 5)
    {
        

        throw new NotImplementedException();
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