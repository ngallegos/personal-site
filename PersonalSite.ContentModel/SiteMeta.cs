using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Personal site metadata", Name = "SiteMetaData", 
    DisplayField = "domain", Id = "personalSiteMetaData")]
public class SiteMetaData
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "domain", Required = true, Name = "Domain")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Domain { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "navLinks", ItemsLinkType = SystemLinkTypes.Entry)]
    [FieldAppearance(SystemWidgetIds.EntryMultipleLinksEditor)]
    public List<Link> NavLinks { get; set; } = new List<Link>();
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "contactLinks", ItemsLinkType = SystemLinkTypes.Entry)]
    [FieldAppearance(SystemWidgetIds.EntryMultipleLinksEditor)]
    public List<Link> ContactLinks { get; set; } = new List<Link>();
}