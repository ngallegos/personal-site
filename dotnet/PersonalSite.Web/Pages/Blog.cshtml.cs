using Microsoft.AspNetCore.Mvc;
using PersonalSite.ContentModel;
using PersonalSite.ContentModel.Blog;

namespace PersonalSite.Web.Pages;

public class BlogModel(IContentService contentService) : BlogBase(contentService)
{
    private const int PageSize = 10;

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string? Tag { get; set; }

    [BindProperty]
    public List<Post> Posts { get; set; } = new();

    protected override async Task<IActionResult> GetResultAsync()
    {
        var skip = (Math.Max(1, CurrentPage) - 1) * PageSize;
        Posts = await _contentService.GetBlogPostsAsync(Domain, Tag, skip, PageSize);
        return Page();
    }
}
