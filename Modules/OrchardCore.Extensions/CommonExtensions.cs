using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace OrchardCore.Extensions
{
    public static class CommonExtensions
    {
        public static T GetService<T>(this ControllerBase controller)
        {
            return controller.HttpContext.RequestServices.GetService<T>();
        }

    }
}
