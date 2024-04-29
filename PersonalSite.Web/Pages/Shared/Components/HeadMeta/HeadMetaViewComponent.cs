using Microsoft.AspNetCore.Mvc;
using PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages.Shared.Components.HeadMeta;

public class HeadMetaViewComponent : ViewComponent
{
    private readonly IContentService _contentService;
    
    public HeadMetaViewComponent(IContentService contentService)
    {
        _contentService = contentService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var domain = Request.Host.Value.Split(':')[0].ToLower();
        var siteMeta = await _contentService.GetSiteMetaDataAsync(domain);
        return View(siteMeta);
    }
}