using Contentful.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.Web.Extensions;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class BlogPostModel : PageModel
{
    private readonly CT.IContentService _contentService;
    
    [BindProperty(SupportsGet = true)]
    public string? Slug { get; set; }
    
    [BindProperty]
    public CT.Blog.Post? Post { get; set; }
    
    public BlogPostModel(CT.IContentService contentService)
    {
        _contentService = contentService;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        var slug = Slug ?? "home";
        slug = "/" + slug.ToLower().TrimStart('/');
        Post = await _contentService.GetBlogPostAsync(this.GetRequestDomain(), slug);
        if (Post == null)
            return NotFound();

        return Page();
    }
}