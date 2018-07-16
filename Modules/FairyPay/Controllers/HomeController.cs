using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eternity.DependencyInjection.Extensions;
using FairyPay.Models;
using FairyPay.PaymentProviders;
using FairyPay.PaymentProviders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data;
using OrchardCore.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FairyPay.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {


        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var a = this.GetService<IKeyedServicesFactory<string, IPaymentServicesProvider>>();

            var aa = a.GetService("zfpay", default(ProviderSettings));
            aa.Request(default(PayRequestModel));
            return Content(aa.ToString());
        }
    }
}
