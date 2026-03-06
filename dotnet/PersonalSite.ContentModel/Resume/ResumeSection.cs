using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Section to display on a resume", Name = "Resume Section", 
    DisplayField = "heading", Id = "personalSiteResumeSection")]
public class ResumeSection
{
    [IgnoreContentField]
    [JsonIgnore]
    public SystemProperties? Sys { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "heading", Required = true, Name = "Heading")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Heading { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "subHeading", Name = "Sub-heading")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? SubHeading { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "category", Required = true, Name = "Category")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Category { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Text, Id = "content", Required = true, Name = "Content")]
    [FieldAppearance(SystemWidgetIds.Markdown)]
    public string? Content { get; set; }
    
    [IgnoreContentField]
    public HtmlString? HtmlContent => Content?.ToHtmlString();
}