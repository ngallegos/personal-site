using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Link", Name = "Link", 
    DisplayField = "slug", Id = "personalSiteLink")]
public class Link
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "slug", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SlugEditor)]
    [Unique]
    public string? Slug { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "text", Required = true, Name = "Text")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Text { get; set; }
}