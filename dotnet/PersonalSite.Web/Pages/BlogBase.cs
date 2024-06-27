using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.ContentModel;
using PersonalSite.Web.Extensions;

namespace PersonalSite.Web.Pages;

public abstract class BlogBase(IContentService contentService) : PageBase(contentService)
{
    protected override async Task<SiteMetaData?> GetMetaDataAsync()
    {
        // TODO - return blog specific metadata
        return await _contentService.GetSiteMetaDataAsync(Domain);
    }
}