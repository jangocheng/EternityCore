using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyPay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Data;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Extensions;
using OrchardCore.Navigation;
using OrchardCore.Settings;

namespace FairyPay.Controllers
{
    public class ProvidersAdminController : Controller
    {
        private readonly INotifier _notifier;
        private readonly ISiteService _siteService;

        public ProvidersAdminController(INotifier notifier,
            ISiteService siteService)
        {
            _notifier = notifier;
            _siteService = siteService;
        }

        public async Task<IActionResult> IndexAsync(PagerParameters pagerParameters)
        {
            var siteSettings = await _siteService.GetSiteSettingsAsync();
            var pager = new Pager(pagerParameters, siteSettings.PageSize);
            var results = this.GetService<DBContext>().Set<PayProviderSettings>().Skip(pager.GetStartIndex()).Take(pager.PageSize)
                 .ToListAsync();

            var a = await this.GetService<IShapeFactory>().CreateAsync("Pager");
            a.Metadata.
        }
    }
}
