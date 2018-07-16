using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace OrchardCore.Mvc.DeviceDetector
{
    public static class DeviceType
    {
        public static string Pc = "";
        public static string Mobile = nameof(Mobile);
        public static string Pad = nameof(Pad);
    }

    public class DeviceOptions
    {
        internal readonly IDictionary<string, Func<HttpRequest, bool>> Dectetors =
            new Dictionary<string, Func<HttpRequest, bool>>(StringComparer.InvariantCultureIgnoreCase);

        public DeviceOptions()
        {

        }


        public void AddDetector(string deviceType, Func<HttpRequest, bool> dectetor)
        {
            Dectetors[deviceType] = dectetor;
        }
    }
}
