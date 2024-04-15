using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class ResumeModel : PageModel
{
    private readonly IContentService _contentService;
    
    [BindProperty]
    public Resume? Resume { get; set; }
    
    public ResumeModel(IContentService contentService)
    {
        _contentService = contentService;
    }
    
    public async Task OnGetAsync()
    {
        Resume = await _contentService.GetResumeAsync();
    }
}