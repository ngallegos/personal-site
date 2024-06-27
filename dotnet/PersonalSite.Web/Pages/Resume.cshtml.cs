using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.ContentModel;
using PersonalSite.Web.Extensions;

namespace PersonalSite.Web.Pages;

public class ResumeModel(IContentService contentService) : PageBase(contentService)
{
    private readonly IContentService _contentService = contentService;
    
    [BindProperty]
    public Resume? Resume { get; set; }

    protected override async Task<IActionResult> GetResultAsync()
    {
        Resume = await _contentService.GetResumeAsync(Domain);
        return Page();
    }

    protected override Task<SiteMetaData?> GetMetaDataAsync()
    {
        // No metadata for the resume page
        return new ValueTask<SiteMetaData?>().AsTask();
    }
}