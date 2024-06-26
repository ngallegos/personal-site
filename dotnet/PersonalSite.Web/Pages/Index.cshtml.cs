﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class IndexModel : PageModel
{
    private readonly CT.IContentService _contentService;
    
    [BindProperty]
    public CT.Page? HomePage { get; set; }

    public IndexModel(CT.IContentService contentService)
    {
        _contentService = contentService;
    }

    public async Task OnGetAsync()
    {
        HomePage = await _contentService.GetPageAsync("/home");
    }
}