using Microsoft.AspNetCore.Mvc;
using PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class BlogModel(IContentService contentService) : BlogBase(contentService)
{
    protected override Task<IActionResult> GetResultAsync()
    {
        return new ValueTask<IActionResult>(Page()).AsTask();
    }
}