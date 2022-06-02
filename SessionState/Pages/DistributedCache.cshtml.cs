using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

namespace Caching.Pages
{
    public class DistributedCacheModel : PageModel
    {
        readonly IDistributedCache _cache;

        public DistributedCacheModel(IDistributedCache cache)
        {
            _cache = cache;
        }

        public DateTime CurrentTime { get; set; }

        public DateTime CachedTime { get; set; }

        public async Task OnGet()
        {
            const string key = nameof(CachedTime);

            CurrentTime = DateTime.UtcNow;

            var cachedTime = await _cache.GetStringAsync(key);
            if (cachedTime == null)
            {
                cachedTime = DateTime.UtcNow.ToString();
                await _cache.SetStringAsync(
                    key, 
                    cachedTime, 
                    new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(30) });
            }

            CachedTime = DateTime.Parse(cachedTime);
        }
    }
}
