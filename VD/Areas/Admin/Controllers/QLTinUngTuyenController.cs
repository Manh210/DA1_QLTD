using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Areas.Admin.Controllers
{
    public class QLTinUngTuyenController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public QLTinUngTuyenController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: Admin/QLTinUngTuyen
        public ActionResult Home_QLTUT(string timTUT = null)
        {
            List<TinUngTuyen> tinUngTuyens = db.TinUngTuyens.Where(tut => tut.Xoa == false).ToList();
            if (!string.IsNullOrEmpty(timTUT))
            {
                tinUngTuyens = db.TinUngTuyens.Where(tut => tut.HoTen.Contains(timTUT)).ToList();
            }
            return View(tinUngTuyens);
        }
        private void LoadViewBags()
        {
            ViewBag.UngViens = db.UngViens.ToList();
            ViewBag.DiaDiems = db.DiaDiems.ToList();
            ViewBag.NganhNghes = db.NganhNghes.ToList();
            ViewBag.LinhVucs = db.LinhVucs.ToList();
        }
        public ActionResult Edit_TT(int id)
        {
            var tinUngTuyen = db.TinUngTuyens.Find(id);
            LoadViewBags();
            return View(tinUngTuyen);
        }
        [HttpPost]
        public ActionResult Edit_TT(TinUngTuyen tinUngTuyen)
        {
            db.TinUngTuyens.Attach(tinUngTuyen);
            db.Entry(tinUngTuyen).Property(model => model.TrangThaiTUT).IsModified = true;
            LoadViewBags();
            db.SaveChanges();
            return RedirectToAction("Home_QLTUT");
        }
        //[HttpPost]
        //public JsonResult UpdateStatus(int id, string status)
        //{
        //    try
        //    {
        //        var tinUngTuyen = db.TinUngTuyens.Find(id);
        //        if (tinUngTuyen == null)
        //        {
        //            return Json(new { success = false, message = "Không tìm thấy tin tuyển dụng." });
        //        }

        //        tinUngTuyen.TrangThaiTUT = status;
        //        tinUngTuyen.TgianCapNhatTT = DateTime.Now;
        //        db.SaveChanges();

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        public ActionResult Delete(int id)
        {
            var tut = db.TinUngTuyens.Find(id);
            if (tut == null)
            {
                return HttpNotFound();
            }
            tut.Xoa = true;
            db.Entry(tut).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Home_QLTUT");
        }
    }
}