using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;
using MusicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MusicStore.Web.FeatureManagement
{
    public class MusicStoreDisabledFeaturesHandler : IDisabledFeaturesHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {
            var vm = new DisabledFeatureViewModel
            {
                FeatureName = string.Join(", ", features),
                GoLiveDate = DateTimeOffset.UtcNow.AddDays(30)
            };

            var viewResult = new ViewResult
            {
                ViewName = "Views/Error/DisabledFeature.cshtml",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = vm
                }
            };

            context.Result = viewResult;
            return Task.CompletedTask;
        }
    }
}
