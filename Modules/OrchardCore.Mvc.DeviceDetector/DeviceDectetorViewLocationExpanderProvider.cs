using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OrchardCore.Mvc.LocationExpander;

namespace OrchardCore.Mvc.DeviceDetector
{
    public class DeviceDectetorViewLocationExpanderProvider : IViewLocationExpanderProvider
    {
        private const string ValueKey = "device";
        private readonly DeviceOptions _deviceOptions;

        public int Priority => 99;

        public DeviceDectetorViewLocationExpanderProvider(IOptions<DeviceOptions> deviceOptions)
        {
            _deviceOptions = deviceOptions.Value;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (!context.Values.TryGetValue(ValueKey, out var deviceId)) return viewLocations;
            return PageViewLocations();
            IEnumerable<string> PageViewLocations()
            {
                foreach (var view in viewLocations)
                {
                    yield return view.Replace("{0}", "{0}." + deviceId);
                    yield return view;
                }
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var request = context.ActionContext.HttpContext.Request;
            foreach (var d in _deviceOptions.Dectetors)
            {
                if (!d.Value(request)) continue;
                context.Values.Add(ValueKey, d.Key);
                break;
            }
        }
    }
}
