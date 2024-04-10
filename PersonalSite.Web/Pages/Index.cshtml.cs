using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.ContentModel;
using CT = PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IContentfulClient _contentful;
    
    [BindProperty]
    public CT.Page HomePage { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IContentfulClient contentful)
    {
        _logger = logger;
        _contentful = contentful;
    }

    public async Task OnGetAsync()
    {
        var query = QueryBuilder<CT.Page>.New.FieldEquals(x => x.Slug, "/home");
        HomePage = (await _contentful.GetEntriesByType<CT.Page>(query)).Single();
    }
}