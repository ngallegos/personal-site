using Microsoft.AspNetCore.Mvc;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class BlogPostModel(CT.IContentService contentService) : BlogBase(contentService)
{
    private readonly CT.IContentService _contentService = contentService;
    
    [BindProperty(SupportsGet = true)]
    public string? Slug { get; set; }
    
    [BindProperty]
    public CT.Blog.Post? Post { get; set; }

    protected override async Task<IActionResult> GetResultAsync()
    {
        var slug = Slug ?? "home";
        slug = "/" + slug.ToLower().TrimStart('/');
        Post = await _contentService.GetBlogPostAsync(Domain, slug);
        if (Post == null)
            return NotFound();

        return Page();
    }
}