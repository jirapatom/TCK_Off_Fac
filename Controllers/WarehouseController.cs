using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using TCK_Off_Fac.Models;
using System.Web.Script.Serialization;
using System.Data.Entity.SqlServer;
using Rotativa;
using System.IO;
using System.Drawing;
using ZXing;
using System.Web;

namespace TCK_Off_Fac.Controllers
{
    public class WarehouseController : Controller
    {
        private TCK_Off_FacEntities DbFile = new TCK_Off_FacEntities();
        public ActionResult Item()
        {

            return View();
        }
        public ActionResult Overview()
        {

            return View();
        }
        public ActionResult MoveLocation()
        {

            return View();
        }
        public ActionResult WHReserve()
        {

            return View();
        }
        public ActionResult Return()
        {

            return View();
        }
        public ActionResult Receive()
        {
            Session["ITEM_NAME"] = null;
            ViewBag.selectType = DbFile.Master_Type.ToList();
            ViewBag.selectFac = DbFile.Master_Site.ToList();
            int Bar = 1000001;
            var CheckBar = DbFile.Master_Item.OrderByDescending(x => x.Barcode).Take(1).SingleOrDefault();
            if (CheckBar != null)
            {
                Bar = int.Parse(CheckBar.Barcode) + 1;
            }
            ViewBag.Barcode = Bar.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult LoadItem()
        {
            JsonResult result = new JsonResult();
            try
            {
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                var preFix = Request.Form.GetValues("columns[1][search][value]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
                var data = (from IT in DbFile.Master_Item.DefaultIfEmpty()
                            join TY in DbFile.Master_Type on IT.Type equals TY.Type_Code into TYY
                            from TY1 in TYY


                            join SII in DbFile.Master_Site on IT.Site equals SII.Site_Code into SIIT
                            from SII1 in SIIT.DefaultIfEmpty()

                            join User in DbFile.Master_Member on IT.User equals User.Code_Mem into Users
                            from User1 in Users.DefaultIfEmpty()
                            select new
                            {
                                IT.Item_No,
                                IT.Item_Name,
                                IT.Item_Des,
                                SII1.Site_Name,
                                Item_Create = SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", IT.Item_Create).Trim().Length) + SqlFunctions.DateName("dd", IT.Item_Create) + SqlFunctions.Replicate("/", 2 - SqlFunctions.StringConvert((double)IT.Item_Create.Value.Month).TrimStart().Length) + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)IT.Item_Create.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)IT.Item_Create.Value.Month).TrimStart() + SqlFunctions.Replicate("/", 2 - SqlFunctions.StringConvert((double)IT.Item_Create.Value.Month).TrimStart().Length) + SqlFunctions.DateName("year", IT.Item_Create).Trim(),
                                IT.Barcode,
                                TY1.Type_Name,
                                User1.Mem_Name,
                                Status = IT.Status.Equals("T") ? "โอนเข้าคลังแล้ว" : (IT.Status.Equals("R") ? "รอโอนเข้าคลัง" : null),
                                IT.Item_Img
                            }).ToList();
                int totalRecords = data.Count;
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    data = data.Where(p => p.Item_No.ToString().ToLower().Contains(search.ToLower()) ||
                        p.Item_Name.ToString().Contains(search.ToLower()) ||
                        p.Item_Des.ToString().Contains(search.ToLower()) ||
                        p.Item_Create.ToString().Contains(search.ToLower()) ||
                        p.Barcode.ToString().Contains(search.ToLower()) ||
                        p.Type_Name.ToString().Contains(search.ToLower()) ||
                        p.Type_Name.ToString().Contains(search.ToUpper()) ||
                        p.Mem_Name.ToString().Contains(search.ToLower()) ||
                        p.Mem_Name.ToString().Contains(search.ToUpper()) ||
                        p.Status.ToString().Contains(search.ToLower()) ||
                        p.Site_Name.ToString().Contains(search.ToLower()) ||
                        p.Site_Name.ToString().Contains(search.ToUpper())
                     ).ToList();
                }
                if (!string.IsNullOrEmpty(preFix))
                {
                    data = data.Where(a => a.Item_No.ToString().ToLower().Contains(preFix.ToLower())).ToList();
                }
                switch (order)
                {
                    case "0":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Item_No).ToList()
                            : data.OrderBy(p => p.Item_No).ToList();
                        break;
                    case "1":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Item_Name).ToList()
                            : data.OrderBy(p => p.Item_Name).ToList();
                        break;
                    case "2":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Item_Des).ToList()
                            : data.OrderBy(p => p.Item_Des).ToList();
                        break;
                    case "3":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Item_Create).ToList()
                            : data.OrderBy(p => p.Item_Create).ToList();
                        break;
                    case "4":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Barcode).ToList()
                            : data.OrderBy(p => p.Barcode).ToList();
                        break;
                    case "5":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Type_Name).ToList()
                            : data.OrderBy(p => p.Type_Name).ToList();
                        break;
                    case "6":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Mem_Name).ToList()
                            : data.OrderBy(p => p.Mem_Name).ToList();
                        break;
                    case "7":
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Site_Name).ToList()
                            : data.OrderBy(p => p.Site_Name).ToList();
                        break;
                    default:
                        data = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                            data.OrderByDescending(p => p.Status).ToList()
                            : data.OrderBy(p => p.Status).ToList();
                        break;
                }

                int recFilter = data.Count;
                data = data.Skip(startRec).Take(pageSize).ToList();
                var modifiedData = data.Select(d =>
                    new
                    {
                        d.Item_No,
                        d.Item_Name,
                        d.Item_Des,
                        d.Item_Create,
                        d.Barcode,
                        d.Type_Name,
                        d.Mem_Name,
                        d.Status,
                        d.Item_Img,
                        d.Site_Name
                    }
                    );
                result = this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = modifiedData
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
        public string CheckItemNo(string ITEM_NO)
        {
            try
            {
                var data = (from MS_I in DbFile.Master_Item
                            where MS_I.Item_No.Equals(ITEM_NO)
                            select new
                            {
                                MS_I
                            }).ToList();
                string jsonlog = new JavaScriptSerializer().Serialize(data);
                return jsonlog;
            }
            catch { return null; }
        }
        //  [HttpPost]
        public ActionResult PrintViewToPdf(string id)
        {
            try
            {
                var data = DbFile.Master_Item.Where(a => a.Barcode.Equals(id)).FirstOrDefault();
                //   ViewBag.WMS_User = DbFile.WMS_PD_Master_User.Where(x => x.Code_Mem.Equals(data.PRO_Recive)).ToList();
                //  var report = new PartialViewAsPdf("~/Views/Stock/Form.cshtml", data);
                return new PartialViewAsPdf("~/Views/Warehouse/FormPrinBar.cshtml", data)
                {
                    PageSize = Rotativa.Options.Size.A4,
                    PageOrientation = Rotativa.Options.Orientation.Landscape,
                    PageMargins = { Top = 1, Bottom = 0 },
                    PageHeight = 100,
                    PageWidth = 30

                };

            }
            catch { return null; }
        }

        public ActionResult RenderBarcode(string id)
        {
            // Session["aaaa"] = null;
            Image img = null;
            using (var ms = new MemoryStream())
            {

                var writer = new ZXing.BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
                writer.Options.Height = 70;
                writer.Options.Width = 600;
                writer.Options.PureBarcode = true;
                img = writer.Write(id);
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                return File(ms.ToArray(), "image/jpeg");
            }
        }

        public string CheckItemNoReceive(string ITEM_NO)
        {
            try
            {
                var data = (from MS_I in DbFile.Master_Item
                            where MS_I.Item_No.Equals(ITEM_NO)
                            select new
                            {
                                MS_I
                            }).ToList();
                string jsonlog = new JavaScriptSerializer().Serialize(data);
                return jsonlog;
            }
            catch { return null; }
        }

        [HttpPost]
        public string CreateRC(string ITEM_NO, string ITEM_NAME, string ITEM_DES, string BARCODE, string REF, string FAC, string TYPE,string USER)
        {
            Session["ITEM_NAME"] = ITEM_NAME;
            try
            {

                var User = DbFile.Master_Member.Where(a => a.Mem_Name.Equals(USER)).FirstOrDefault();
                var MS_ITEM = new Master_Item();
                MS_ITEM.Item_No = ITEM_NO;
                MS_ITEM.Item_Name = ITEM_NAME;
                MS_ITEM.Item_Des = ITEM_DES;
                MS_ITEM.Item_Create = DateTime.Now;
                MS_ITEM.Barcode = BARCODE;
                MS_ITEM.Site = FAC;
                MS_ITEM.Type = TYPE;
                MS_ITEM.User = User.Code_Mem;
                MS_ITEM.Status = "R";
                MS_ITEM.Item_Img = ITEM_NAME + ".jpg";
                DbFile.Master_Item.Add(MS_ITEM);
                DbFile.SaveChanges();
                return "S";

            }
            catch { return "N"; }
        }

        //    [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file, string AA)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/img/Item"),
                    Path.GetFileName(Session["ITEM_NAME"].ToString() + ".jpg"));
                    file.SaveAs(path);

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }

            return null;

        }




    }
}