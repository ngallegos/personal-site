using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Personal site metadata", Name = "SiteMetaData", 
    DisplayField = "siteName", Id = "personalSiteMetaData")]
public class SiteMetaData
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "siteName", Required = true, Name = "Site Name")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    [Unique]
    public string? SiteName { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "domains", Required = true, Name = "Domains")]
    [FieldAppearance(SystemWidgetIds.ListInput, HelpText = "Comma separated list of domains")]
    public List<string> Domains { get; set; } = new List<string>();
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "navLinks", ItemsLinkType = SystemLinkTypes.Entry)]
    [FieldAppearance(SystemWidgetIds.EntryMultipleLinksEditor)]
    public List<Link> NavLinks { get; set; } = new List<Link>();
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "contactLinks", ItemsLinkType = SystemLinkTypes.Entry)]
    [FieldAppearance(SystemWidgetIds.EntryMultipleLinksEditor)]
    public List<Link> ContactLinks { get; set; } = new List<Link>();
    
    [ContentField(Type = SystemFieldTypes.Text, Id = "aboutMe", Required = true, Name = "AboutMe")]
    [FieldAppearance(SystemWidgetIds.Markdown)]
    public string? AboutMe { get; set; }
}