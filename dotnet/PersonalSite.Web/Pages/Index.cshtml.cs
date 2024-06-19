﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class IndexModel : PageModel
{
    private readonly CT.IContentService _contentService;
    
    [BindProperty(SupportsGet = true)]
    public string? Slug { get; set; }
    
    [BindProperty]
    public CT.Page? HomePage { get; set; }

    public IndexModel(CT.IContentService contentService)
    {
        _contentService = contentService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var slug = Slug ?? "home";
        slug = "/" + slug.ToLower().TrimStart('/');
        HomePage = await _contentService.GetPageAsync(Request.Host.Value.Split(':')[0].ToLower(), slug);
        if (HomePage == null)
            return NotFound();

        return Page();
    }
}