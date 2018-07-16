using System;
using FairyPay.Drivers;
using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Navigation;

namespace FairyPay
{
    public class AdminMenu : INavigationProvider
    {
        public IStringLocalizer T { get; set; }
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }
        public void BuildNavigation(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            builder
              .Add(T["爱支付"], "1", main => main
                  .Add(T["设置"], "1", settings => settings
                        .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = PaySettingsDisplayDriver.GroupId })
                      .LocalNav().Permission(Permissions.SettingsAccess))
                    .Add(T["类型"], "2", type => type
                        .Action("Types", "Admin", new { area = "FairyPay" })
                        .Permission(Permissions.PayTypeListAccess))
                    .Add(T["渠道"], "3", channel => channel
                        .Action("Channels", "Admin", new { area = "FairyPay" }).Permission(Permissions.ChannelsListAccess))
              );
        }



    }
}
