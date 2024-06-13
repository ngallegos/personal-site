using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Newtonsoft.Json;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Link", Name = "Link", 
    DisplayField = "slug", Id = "personalSiteLink")]
public class Link
{
    [IgnoreContentField]
    [JsonIgnore]
    public SystemProperties? Sys { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "slug", Required = true, Name = "Url Slug")]
    [FieldAppearance(SystemWidgetIds.SlugEditor)]
    [Unique]
    public string? Slug { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "text", Required = true, Name = "Text")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Text { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Boolean, Id = "external", Name = "External Link")]
    [BooleanAppearance("Yes", "No", "Should the link open in a new tab?")]
    public bool? External { get; set; }
}