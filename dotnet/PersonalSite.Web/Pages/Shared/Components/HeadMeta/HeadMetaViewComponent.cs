using Microsoft.AspNetCore.Mvc;
using PersonalSite.ContentModel;
using PersonalSite.Web.Extensions;

namespace PersonalSite.Web.Pages.Shared.Components.HeadMeta;

public class HeadMetaViewComponent : ViewComponent
{
    private readonly IContentService _contentService;
    
    public HeadMetaViewComponent(IContentService contentService)
    {
        _contentService = contentService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(SiteMetaData? siteMeta = null)
    {
        if (siteMeta == null)
        {
            var domain = this.GetRequestDomain();
            siteMeta = await _contentService.GetSiteMetaDataAsync(domain);
        }
        return View(siteMeta);
    }
}