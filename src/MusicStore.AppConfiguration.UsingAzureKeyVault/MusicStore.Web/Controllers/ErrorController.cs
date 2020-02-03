using Microsoft.AspNetCore.Mvc;
using MusicStore.Web.Models;

namespace MusicStore.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult DisabledFeature()
        {
            var vm = new DisabledFeatureViewModel();
            return View(vm);
        }
    }
}