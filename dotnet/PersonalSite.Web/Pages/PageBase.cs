using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.ContentModel;
using PersonalSite.Web.Extensions;

namespace PersonalSite.Web.Pages;

public abstract class PageBase(IContentService contentService) : PageModel
{
    protected readonly IContentService _contentService = contentService;
    
    protected string Domain => Request.GetDomain();

    [BindProperty]
    public SiteMetaData? MetaData { get; set; }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var gettingMetaData = GetMetaDataAsync();
        var gettingResult = GetResultAsync();
        MetaData = await gettingMetaData;
        return await gettingResult;
    }
    
    protected abstract Task<IActionResult> GetResultAsync();
    
    protected virtual async Task<SiteMetaData?> GetMetaDataAsync()
    {
        return await _contentService.GetSiteMetaDataAsync(Domain);
    }
}