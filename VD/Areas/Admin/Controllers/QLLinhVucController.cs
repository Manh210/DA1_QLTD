using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Areas.Admin.Controllers
{
    public class QLLinhVucController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public QLLinhVucController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: Admin/QLLinhVuc
        public ActionResult Home_LinhVuc(string timLV = null)
        {
            List<LinhVuc> linhVucs = db.LinhVucs.Where(lv => lv.Xoa == false).ToList();
            if (!string.IsNullOrEmpty(timLV))
            {
                linhVucs = db.LinhVucs.Where(dd => dd.TenLV.Contains(timLV)).ToList();
            }
            return View(linhVucs);
        }
        public ActionResult Add_LV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_LV(LinhVuc linhVuc)
        {
            linhVuc.Xoa = false;
            db.LinhVucs.Add(linhVuc);
            db.SaveChanges();
            return RedirectToAction("Home_LinhVuc");
        }
        public ActionResult Edit_LV(int id)
        {
            var linhVuc = db.LinhVucs.Find(id);
            return View(linhVuc);
        }
        [HttpPost]
        public ActionResult Edit_LV(LinhVuc linhVuc)
        {
            db.LinhVucs.Attach(linhVuc);
            db.Entry(linhVuc).Property(model => model.TenLV).IsModified = true;
            db.Entry(linhVuc).Property(model => model.MotaLV).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Home_LinhVuc");
        }
        //[HttpPost]
        //public ActionResult Delete_LV(int id)
        //{
        //    var linhVuc = db.LinhVucs.Find(id);
        //    if (linhVuc != null)
        //    {
        //        db.LinhVucs.Remove(linhVuc);
        //        db.SaveChanges();
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });
        //}
        public ActionResult Delete(int id)
        {
            var linhVuc = db.LinhVucs.Find(id);
            if (linhVuc == null)
            {
                return HttpNotFound();
            }
            linhVuc.Xoa = true;
            db.Entry(linhVuc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Home_LinhVuc");
        }
    }
}