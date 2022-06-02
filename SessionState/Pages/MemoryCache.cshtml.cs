using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Pages
{
    public class MemoryCacheModel : PageModel
    {
        readonly IMemoryCache _cache;

        public MemoryCacheModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        public DateTime CurrentTime { get; set; }

        public DateTime CachedTime { get; set; }

        public void OnGet()
        {
            const string key = nameof(CachedTime);

            CurrentTime = DateTime.UtcNow;
            
            if (!_cache.TryGetValue(key, out DateTime cachedTime))
            {
                cachedTime = DateTime.UtcNow;
                _cache.Set(key, cachedTime, 
                    new MemoryCacheEntryOptions 
                    { 
                        SlidingExpiration = TimeSpan.FromSeconds(5) 
                    });
            }

            CachedTime = cachedTime;
        }
    }
}
