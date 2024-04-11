using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Html;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Single page content", Name = "Page", 
    DisplayField = "slug", Id = "personalSitePage")]
public class Page
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "slug", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SlugEditor)]
    [Unique]
    public string? Slug { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Text, Id = "content", Required = true, Name = "Content")]
    [FieldAppearance(SystemWidgetIds.Markdown)]
    public string? Content { get; set; }
    
    [IgnoreContentField]
    public HtmlString? HtmlContent => Content?.ToHtmlString();
        
    [ContentField(Type = SystemFieldTypes.Link, Id = "featuredImage", LinkType = SystemLinkTypes.Asset, Name = "Featured Image")]
    [FieldAppearance(SystemWidgetIds.AssetLinkEditor)]
    [MimeType(MimeTypes = new [] {MimeTypeRestriction.Image})]
    public Asset? FeaturedImage { get; set; }
}