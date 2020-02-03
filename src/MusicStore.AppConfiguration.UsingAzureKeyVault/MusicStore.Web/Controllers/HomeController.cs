using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStore.Web.Models;
using MusicStore.Services;
using Microsoft.FeatureManagement;
using MusicStore.Shared.FeatureManagement;
using System.Threading.Tasks;
using Microsoft.FeatureManagement.Mvc;
using MusicStore.Shared;
using Microsoft.Extensions.Options;

namespace MusicStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAlbumService _albumService;
        private readonly IFeatureManager _featureManager;
        private readonly AppSettings _appSettings;

        public HomeController(IAlbumService albumService, IFeatureManagerSnapshot featureManager, IOptionsSnapshot<AppSettings> options, ILogger<HomeController> logger)
        {
            _albumService = albumService;
            _featureManager = featureManager;
            _appSettings = options.Value;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();
            vm.Featured = _albumService.AllAlbums();

            if (await _featureManager.IsEnabledAsync(Features.UserSuggestions))
            {
                vm.Suggestions = _albumService.UserPreferenceAlbums();
            }

            return View(vm);
        }

        public IActionResult Albums()
        {
            var albums = _albumService.AllAlbums();
            return View(albums);
        }

        [FeatureGate(Features.Promotions)]
        public async Task<IActionResult> Promotions()
        {
            var promoAlbums = await _albumService.PromotionalAlbumsAsync();
            return View(promoAlbums);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
