using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Newtonsoft.Json;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Head Meta Tag", Name = "Head Meta", 
    DisplayField = "name", Id = "personalSiteHeadMeta")]
public class HeadMeta
{
    [IgnoreContentField]
    [JsonIgnore]
    public SystemProperties? Sys { get; set; }
        
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "name", Required = true, Name = "Name")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    [Unique]
    public string? Name { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "content", Required = true, Name = "Content")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Content { get; set; }
}