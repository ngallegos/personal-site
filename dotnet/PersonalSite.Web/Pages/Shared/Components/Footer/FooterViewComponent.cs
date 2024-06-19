using Microsoft.AspNetCore.Mvc;
using PersonalSite.ContentModel;
using PersonalSite.Web.Extensions;

namespace PersonalSite.Web.Pages.Shared.Components.Footer;

public class FooterViewComponent : ViewComponent
{
    private readonly IContentService _contentService;
    
    public FooterViewComponent(IContentService contentService)
    {
        _contentService = contentService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var domain = this.GetRequestDomain();
        var siteMeta = await _contentService.GetSiteMetaDataAsync(domain);
        return View(siteMeta);
    }
}