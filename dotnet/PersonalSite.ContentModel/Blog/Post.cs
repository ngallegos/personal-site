using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Contentful.Core.Search;

namespace PersonalSite.ContentModel.Blog;

[ContentType(Description = "Blog Post", Name = "Post", DisplayField = "title", Id = "personalSiteBlogPost", Order = 2)]
public class Post
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "title", Required = true, Name = "Title")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Title { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "slug", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SlugEditor)]
    [Unique]
    public string? Slug { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Text, Id = "content", Required = true, Name = "Content")]
    [FieldAppearance(SystemWidgetIds.Markdown)]
    public string? Content { get; set; }

    [ContentField(Type = SystemFieldTypes.Array, Id = "tags", ItemsLinkType = SystemLinkTypes.Entry)]
    [FieldAppearance(SystemWidgetIds.TagEditor)]
    [LinkContentType("tag")]
    public List<Tag> Tags { get; set; } = new List<Tag>();
    
    [ContentField(Type = SystemFieldTypes.Boolean, Id = "sticky", Required = true, Name = "Sticky")]
    [BooleanAppearance("Yes", "No", "Should this post be sticky?")]
    public bool Sticky { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Link, Id = "featuredImage", LinkType = SystemLinkTypes.Asset, Name = "Featured Image")]
    [FieldAppearance(SystemWidgetIds.AssetLinkEditor)]
    [MimeType(MimeTypes = new [] {MimeTypeRestriction.Image})]
    public Asset? FeaturedImage { get; set; }
}