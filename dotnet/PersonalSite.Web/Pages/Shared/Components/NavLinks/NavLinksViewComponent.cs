using Microsoft.AspNetCore.Mvc;
using PersonalSite.ContentModel;
using PersonalSite.Web.Extensions;

namespace PersonalSite.Web.Pages.Shared.Components.NavLinks;

public class NavLinksViewComponent : ViewComponent
{
    private readonly IContentService _contentService;
    
    public NavLinksViewComponent(IContentService contentService)
    {
        _contentService = contentService;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(SiteMetaData? siteMeta = null, NavLinksType? linkType = null, string ulClass = "", string liClass = "")
    {
        var model = new NavLinksViewModel { UlClass = ulClass, LiClass = liClass };
        if (siteMeta == null)
        {
            var domain = this.GetRequestDomain();
            siteMeta = await _contentService.GetSiteMetaDataAsync(domain);
        }

        switch (linkType)
        {
            case NavLinksType.Nav:
                model.Links = siteMeta?.NavLinks ?? new List<Link>();
                break;
            case NavLinksType.Footer:
                model.Links = siteMeta?.ContactLinks ?? new List<Link>();
                break;
            default:
                model.Links = siteMeta?.NavLinks.Concat(siteMeta.ContactLinks).ToList() ?? new List<Link>();
                break;
        }
        return View(model);
    }
}

public enum NavLinksType
{
    Nav,
    Footer
}

public class NavLinksViewModel
{
    public string? UlClass { get; set; }
    public string? LiClass { get; set; }
    public List<Link> Links { get; set; } = new List<Link>();
}