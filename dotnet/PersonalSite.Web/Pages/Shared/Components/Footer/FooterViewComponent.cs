using Microsoft.AspNetCore.Mvc;
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
        var domain = Request.Host.Value.Split(':')[0].ToLower();
        var siteMeta = await _contentService.GetSiteMetaDataAsync(domain);
        return View(siteMeta);
    }
}