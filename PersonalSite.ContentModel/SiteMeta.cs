using Contentful.CodeFirst;
using Contentful.Core.Models.Management;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Personal site metadata", Name = "SiteMetaData", 
    DisplayField = "domain", Id = "personalSiteMetaData")]
public class SiteMetaData
{
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "domain", Required = true, Name = "Domain")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Domain { get; set; }
}