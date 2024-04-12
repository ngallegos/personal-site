using Contentful.CodeFirst;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Microsoft.AspNetCore.Html;

namespace PersonalSite.ContentModel;

[ContentType(Description = "Resume content", Name = "Resume", 
    DisplayField = "resumeName", Id = "personalSiteResume")]
public class Resume
{
    [IgnoreContentField]
    public SystemProperties? Sys { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "resumeName", Required = true, Name = "Resume Name")]
    [FieldAppearance(SystemWidgetIds.SingleLine, HelpText = "The name of the resume instance, not the person's name.")]
    public string? ResumeName { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "name", Required = true, Name = "Name")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Name { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "title", Required = true, Name = "Title")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Title { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "skills", Required = true, Name = "Skills")]
    [FieldAppearance(SystemWidgetIds.TagEditor)]
    public List<string> Skills { get; set; } = new List<string>();
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "location", Name = "Location")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Location { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "email", Required = true, Name = "Email")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Email { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "phone", Name = "Phone")]
    [FieldAppearance(SystemWidgetIds.SingleLine)]
    public string? Phone { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "website", Name = "Website")]
    [FieldAppearance(SystemWidgetIds.UrlEditor)]
    public string? Website { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "gitHub", Name = "GitHub")]
    [FieldAppearance(SystemWidgetIds.UrlEditor)]
    public string? GitHub { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Symbol, Id = "linkedIn", Name = "LinkedIn")]
    [FieldAppearance(SystemWidgetIds.UrlEditor)]
    public string? LinkedIn { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Boolean, Id = "active", Required = true, Name = "Active")]
    [BooleanAppearance("Yes", "No", "Is this the resume that should be used?")]
    public bool Active { get; set; }
    
    [ContentField(Type = SystemFieldTypes.Array, Id = "sections", ItemsLinkType = SystemLinkTypes.Entry)]
    [FieldAppearance(SystemWidgetIds.EntryMultipleLinksEditor)]
    public List<ResumeSection> Sections { get; set; } = new List<ResumeSection>();
}