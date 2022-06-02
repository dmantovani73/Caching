using Caching.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Caching.Pages;

public class SessionStateModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public SessionStateModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public DateTime SessionTime { get; set; }

    public void OnGet()
    {
        const string key = "SessionData";

        var session = HttpContext.Session;

        var data = session.Get<DateTime?>(key);
        if (data == null)
        {
            data = DateTime.UtcNow;
            session.Set(key, data);
        }

        SessionTime = data.Value;
    }
}
