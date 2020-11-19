using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using TCK_Off_Fac.Models;
using System.Web.Script.Serialization;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace TCK_Off_Fac.Controllers
{
    public class ClassController : Controller
    {
        private TCK_Off_FacEntities DbFile = new TCK_Off_FacEntities();
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }
        //CheckUser
        public string CheckUser(string USER, string FUNC)
        {
            try
            {
                var data = (from Ms_M in DbFile.Master_Member
                            where Ms_M.Code_Mem.Equals(USER) && (Ms_M.Mem_Function.Equals(FUNC)||Ms_M.Mem_authorization1.Equals(FUNC) || Ms_M.Mem_authorization2.Equals(FUNC) || Ms_M.Mem_authorization3.Equals(FUNC))
                            select new
                            {
                                Ms_M.Mem_Name
                            }).ToList();
                string jsonlog = new JavaScriptSerializer().Serialize(data);
                return jsonlog;
            }
            catch { return null; }
        }//CheckLocation  //CheckUser
        public string CheckLocation(string LOC)
        {
            try
            {
                var data = (from Ms_L in DbFile.Master_Location
                            where Ms_L.Lo_Code.Equals(LOC)
                            select new
                            {
                                Ms_L.Lo_Name
                            }).ToList();
                string jsonlog = new JavaScriptSerializer().Serialize(data);
                return jsonlog;
            }
            catch { return null; }
        }//
    }
}