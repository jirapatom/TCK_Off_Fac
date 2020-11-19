using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TCK_Off_Fac.Models;

namespace TCK_Off_Fac.Controllers
{
    public class DisbursementController : Controller
    {
        private TCK_Off_FacEntities DbFile = new TCK_Off_FacEntities();
        public ActionResult CreateOrder()
        {
            ViewBag.TyItem = DbFile.Master_Type.ToList();
            return View();
        }

        

    }
}