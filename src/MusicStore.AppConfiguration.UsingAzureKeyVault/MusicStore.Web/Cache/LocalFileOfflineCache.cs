using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace MusicStore.Web.Cache
{
    public class LocalFileOfflineCache : IOfflineCache
    {
        private readonly string _cacheFilePath;

        public LocalFileOfflineCache(IHostEnvironment env)
        {
            _cacheFilePath = Path.Combine(env.ContentRootPath, "offline_cache.json");
        }

        public void Export(AzureAppConfigurationOptions options, string data)
        {
            File.WriteAllText(_cacheFilePath, data);
        }

        public string Import(AzureAppConfigurationOptions options)
        {
            var cacheData = File.ReadAllText(_cacheFilePath);
            return cacheData;
        }
    }
}
