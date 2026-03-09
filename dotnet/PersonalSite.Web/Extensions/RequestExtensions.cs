using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalSite.Web.Extensions;

public static class RequestExtensions
{
    public static string GetRequestDomain(this ViewComponent viewComponent)
    {
        return viewComponent.Request.GetDomain();
    }
    
    public static string GetRequestDomain(this PageModel pageModel)
    {
        return pageModel.Request.GetDomain();
    }
    
    public static string GetDomain(this HttpRequest request)
    {
        return (request.Host.Value ?? string.Empty).Split(':')[0].ToLower();
    }
}