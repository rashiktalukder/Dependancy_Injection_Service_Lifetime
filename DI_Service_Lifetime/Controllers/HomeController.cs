using DI_Service_Lifetime.Interface;
using DI_Service_Lifetime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace DI_Service_Lifetime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IScopedGuidService scoped1;
        private readonly IScopedGuidService scoped2;
        private readonly ISingletonGuidService singleton1;
        private readonly ISingletonGuidService singleton2;
        private readonly ITransientGuidService transient1;
        private readonly ITransientGuidService transient2;

        public HomeController(ILogger<HomeController> logger, 
            IScopedGuidService scoped1, IScopedGuidService scoped2,
            ISingletonGuidService singleton1, ISingletonGuidService singleton2,
            ITransientGuidService transient1, ITransientGuidService transient2)
        {
            _logger = logger;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.singleton1 = singleton1;
            this.singleton2 = singleton2;
            this.transient1 = transient1;
            this.transient2 = transient2;
        }

        public IActionResult Index()
        {
            StringBuilder message = new StringBuilder();

            message.Append($"Transient : after start the application, New Service will create on every request \n");
            message.Append($"Scoped : after start the application, New Service create on once per request. \n");
            message.Append($"Singleton : after start the application, this service will create just one time on application runningtime/lifetime \n\n\n");

            message.Append($"Transient 1 : {transient1.GetGuid()}\n");  //after start the application, New Service create on every request
            message.Append($"Transient 2 : {transient2.GetGuid()}\n\n\n");  //after start the application, New Service create on every request

            message.Append($"Scoped 1 : {scoped1.GetGuid()}\n");    //after start the application, New Service create on once per request. that means, if you refresh, the scope1 and scope2 value will be same.
            message.Append($"Scoped 2 : {scoped2.GetGuid()}\n\n\n");//after start the application, New Service create on once per request. that means, if you refresh, the scope1 and scope2 value will be same.

            message.Append($"Singleton 1 : {singleton1.GetGuid()}\n");  //after start the application, this service will create just one time on application runningtime/lifetime.
            message.Append($"Singleton 2 : {singleton2.GetGuid()}\n\n\n");  //after start the application, this service will create just one time on application runningtime/lifetime.

            return Ok(message.ToString());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
