using Microsoft.AspNetCore.Mvc;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class IndexModel(CT.IContentService contentService) : PageBase(contentService)
{
    [BindProperty(SupportsGet = true)]
    public string? Slug { get; set; }
    
    [BindProperty]
    public CT.Page? HomePage { get; set; }

    protected override async Task<IActionResult> GetResultAsync()
    {
        var slug = Slug ?? "home";
        slug = "/" + slug.ToLower().TrimStart('/');
        HomePage = await _contentService.GetPageAsync(Domain, slug);
        if (HomePage == null)
            return NotFound();

        return Page();
    }
}