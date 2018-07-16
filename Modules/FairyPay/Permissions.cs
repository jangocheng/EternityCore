using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace FairyPay
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission SettingsAccess = new Permission("Settings", "设置");
        public static readonly Permission PayTypeListAccess = new Permission("TypesList", "类型列表");
        public static readonly Permission ChannelsListAccess = new Permission("ChannelsList", "渠道列表");
        public IEnumerable<Permission> GetPermissions()
        {
            return new[] { SettingsAccess, PayTypeListAccess, ChannelsListAccess };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Authenticated",
                    Permissions = GetPermissions()
                }
            };
        }
    }
}
