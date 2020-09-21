using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace VidlyProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //caching the rendered html not the data itself
        [OutputCache(Duration = 50, 
            Location = OutputCacheLocation.Server,    //if cache is specific for a client, we put it on the client, else we cache it on the server
            VaryByParam = "genre")] //for every genre we will have a different version of it in the cache
        public ActionResult Index()
        {
          
            return View();
        }
        //Following code is to disable caching on an action
        [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}