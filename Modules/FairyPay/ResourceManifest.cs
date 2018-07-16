using System;
using System.Collections.Generic;
using System.Text;
using OrchardCore.ResourceManagement;

namespace FairyPay
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(IResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest
                .DefineScript("jQuery")
                .SetCdn("https://code.jquery.com/jquery-3.3.1.min.js")
                .SetVersion("999.3.1"); //使优先级高于Orchard.Resources模块定义的jQuery
        }
    }
}
