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
    
    public async Task<IViewComponentResult> InvokeAsync(SiteMetaData? siteMeta = null, NavLinksType? linkType = null, string ulClass = "", string liClass = "", bool isBlogPage = false)
    {
        var model = new NavLinksViewModel { UlClass = ulClass, LiClass = liClass };
        var domain = this.GetRequestDomain();

        if (siteMeta == null)
            siteMeta = await _contentService.GetSiteMetaDataAsync(domain);

        switch (linkType)
        {
            case NavLinksType.Nav:
                model.Links = siteMeta?.NavLinks ?? new List<Link>();
                if (isBlogPage)
                {
                    model.Links = model.Links.Prepend(new Link { Slug = "/", Text = "Home" }).ToList();
                }
                else
                {
                    var hasPosts = (await _contentService.GetBlogPostsAsync(domain, limit: 1)).Any();
                    if (hasPosts)
                        model.Links = model.Links.Prepend(new Link { Slug = "/blog", Text = "Blog" }).ToList();
                }
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