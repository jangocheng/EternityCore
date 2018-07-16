using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FairyPay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using OrchardCore.Data;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using OrchardCore.Extensions;

namespace FairyPay.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDisplayManager<ISite> _siteSettingsDisplayManager;
        private readonly ISiteService _siteService;
        private readonly INotifier _notifier;
        private readonly IAuthorizationService _authorizationService;
        IHtmlLocalizer H { get; set; }
        public IShapeFactory ShapeFactory { get; }
        public dynamic New { get; private set; }

        public AdminController(
            ISiteService siteService,
            IDisplayManager<ISite> siteSettingsDisplayManager,
            IAuthorizationService authorizationService,
            INotifier notifier,
            IHtmlLocalizer<AdminController> h, IShapeFactory shapeFactory)
        {
            _siteSettingsDisplayManager = siteSettingsDisplayManager;
            _siteService = siteService;
            _notifier = notifier;
            _authorizationService = authorizationService;
            H = h;
            ShapeFactory = shapeFactory;
        }
        public IActionResult Types(PagerParameters pagerParameters)
        {
            var db = this.GetService<DBContext>();
            db.Set<PayType>().ToList();
             return View();
        }

        public IActionResult TypeEdit(string id)
        {
            return View();
        }
    }
}
