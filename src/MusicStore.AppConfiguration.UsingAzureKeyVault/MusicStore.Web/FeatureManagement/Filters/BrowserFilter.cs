using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Web.FeatureManagement.Filters
{
    [FilterAlias(FilterAlias)]
    public class BrowserFilter : IFeatureFilter
    {
        private const string FilterAlias = "MusicStore.Browser";
        private const string Chrome = "Chrome";
        private const string Firefox = "Firefox";
        private const string IE = "MSIE";

        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrowserFilter(IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = loggerFactory.CreateLogger<BrowserFilter>();
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var settings = context.Parameters.Get<BrowserFilterSettings>() ?? new BrowserFilterSettings();

            if (settings.AllowedBrowsers.Any(browser => browser.Contains(Chrome, StringComparison.OrdinalIgnoreCase)) && IsBrowser(Chrome))
            {
                return Task.FromResult(true);
            }

            if (settings.AllowedBrowsers.Any(browser => browser.Contains(Firefox, StringComparison.OrdinalIgnoreCase)) && IsBrowser(Firefox))
            {
                return Task.FromResult(true);
            }

            if (settings.AllowedBrowsers.Any(browser => browser.Contains(IE, StringComparison.OrdinalIgnoreCase)) && IsBrowser(IE))
            {
                return Task.FromResult(true);
            }

            _logger.LogWarning($"The AllowedBrowsers list is empty or the current browser is not enabled for this feature");

            return Task.FromResult(false);
        }

        private string GetUserAgent()
        {
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            return userAgent;
        }

        private bool IsBrowser(string browser)
        {
            var userAgent = GetUserAgent();
            return userAgent != null && userAgent.Contains(browser, StringComparison.OrdinalIgnoreCase);
        }
    }


}
