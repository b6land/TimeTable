using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "專案描述資訊";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "相關聯絡資訊";

            return View();
        }

        [HttpPost]
        public ActionResult InputValue(InputModel model)
        {
            ViewData["Value"] = model.Value;
            return View("Index", model);
        }
    }
}