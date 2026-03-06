using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace PersonalSite.ContentModel.Blog;

[ContentType(Description = "Blog Tag", Name = "Tag", DisplayField = "name", Id = "personalSiteBlogTag")]
public class Tag
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "name", Required = true, Name = "Name")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    [Unique]
    public string? Name { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "slug", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SlugEditor)]
    [Unique]
    public string? Slug { get; set; }
}