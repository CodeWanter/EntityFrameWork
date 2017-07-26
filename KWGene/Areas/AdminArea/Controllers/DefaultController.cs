using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KWGene.Areas.AdminArea.Controllers
{
    public class DefaultController : Controller
    {
        // GET: AdminArea/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}