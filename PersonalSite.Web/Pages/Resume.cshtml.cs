using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalSite.ContentModel;

namespace PersonalSite.Web.Pages;

public class ResumeModel : PageModel
{
    private readonly IContentfulClient _contentful;
    
    [BindProperty]
    public Resume? Resume { get; set; }
    
    public ResumeModel(IContentfulClient contentful)
    {
        _contentful = contentful;
    }
    
    public async Task OnGetAsync()
    {
        var query = QueryBuilder<Resume>.New.FieldEquals(x => x.Active, "true");
        Resume = (await _contentful.GetEntriesByType<Resume>(query)).Single();
    }
}