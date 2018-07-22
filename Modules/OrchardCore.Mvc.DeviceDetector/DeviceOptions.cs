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
        private readonly Dictionary<string, Func<HttpRequest, bool>> _dectetors =
            new Dictionary<string, Func<HttpRequest, bool>>(StringComparer.InvariantCultureIgnoreCase);


        public IReadOnlyDictionary<string, Func<HttpRequest, bool>> Dectetors =>  _dectetors;

        public void AddDetector(string deviceType, Func<HttpRequest, bool> dectetor)
        {
            _dectetors[deviceType] = dectetor;
        }
    }
}
