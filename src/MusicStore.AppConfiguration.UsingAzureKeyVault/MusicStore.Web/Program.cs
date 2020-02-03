using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using MusicStore.Web.Cache;

namespace MusicStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) => {
                    var settings = config.Build();
                    var appConfigEndpoint = settings["AppSettings:AppConfiguration:Endpoint"];
                    var userAssignedIdentityClientId = settings["AppSettings:Identity:ClientId"];

                    if (!string.IsNullOrEmpty(appConfigEndpoint))
                    {
                        var endpoint = new Uri(appConfigEndpoint);

                        config.AddAzureAppConfiguration(options =>
                        {
                            options
                                // Use managed identity to access app configuration
                                .Connect(endpoint, new ManagedIdentityCredential(clientId: userAssignedIdentityClientId))

                                // Setup dynamic refresh
                                .ConfigureRefresh(refreshOpt =>
                                {
                                    refreshOpt.Register(key: "AppSettings:Version", refreshAll: true, label: LabelFilter.Null);
                                    refreshOpt.SetCacheExpiration(TimeSpan.FromSeconds(10));
                                })

                                // Setup offline cache
                                //.SetOfflineCache(new OfflineFileCache())

                                // Configure custom offline cache implementation
                                .SetOfflineCache(new LocalFileOfflineCache(context.HostingEnvironment))

                                // Configure Azure Key Vault with Managed Identity
                                .ConfigureKeyVault(vaultOpt =>
                                {
                                    vaultOpt.SetCredential(new ManagedIdentityCredential(clientId: userAssignedIdentityClientId));
                                })

                                .UseFeatureFlags();
                        });
                    }

                });
    }
}
