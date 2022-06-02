using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Caching.Pages
{
    [ResponseCache(
        Duration = 30, 
        Location = ResponseCacheLocation.Any, 
        NoStore = false, 
        VaryByQueryKeys = new[] { "p" })]
    public class ResponseCachingModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
