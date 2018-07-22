using System.Threading.Tasks;
using FairyPay.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCore.Entities.DisplayManagement;


namespace FairyPay.Drivers
{
    public class PaySettingsDisplayDriver : SectionDisplayDriver<ISite, PaySettings>
    {
        public const string GroupId = "FairyPaySettings";

        public override IDisplayResult Edit(PaySettings section)
        {


            return Initialize<PaySettings>("PaySettings_Edit", model =>
             {
                 model.LoginEnabled = section.LoginEnabled;
             })
               .Location("Content:5")
               .OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(PaySettings section, IUpdateModel updater, string groupId)
        {
            if (groupId == GroupId)
            {
                await updater.TryUpdateModelAsync(section, Prefix);
            }

            return Edit(section);
        }
    }
}
