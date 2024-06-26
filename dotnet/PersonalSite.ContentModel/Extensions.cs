using Contentful.CodeFirst;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Markdig;
using Microsoft.AspNetCore.Html;

namespace PersonalSite.ContentModel;

public static  class Extensions
{
    public static async Task<ContentfulCollection<T>> GetEntriesByType<T>(this IContentfulClient client, 
        QueryBuilder<T>? queryBuilder = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var type = typeof(T);
        var contentTypeID = type.GetContentTypeId();
        return await client.GetEntriesByType<T>(contentTypeID, queryBuilder, cancellationToken);
    }
    
    private static string? GetContentTypeId(this Type type)
    {
        var attr = type.GetCustomAttributes(typeof(ContentTypeAttribute), false).FirstOrDefault() as ContentTypeAttribute;
        return attr?.Id;
    }
    
    public static HtmlString ToHtmlString(this string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        var html = Markdown.ToHtml(markdown, pipeline);
        return new HtmlString(html);
    } 
}