using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Link", Name = "Link", 
    DisplayField = "name", Id = "personalSiteLink")]
public class Link
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "slug", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SlugEditor)]
    public string? Slug { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "name", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    [Unique]
    public string? Name { get; set; }
}