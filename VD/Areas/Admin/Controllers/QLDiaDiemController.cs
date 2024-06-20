using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VD.Models;

namespace VD.Areas.Admin.Controllers
{
    public class QLDiaDiemController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public QLDiaDiemController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: Admin/QLDiaDiem
        public ActionResult Index(string timDD = null)
        {
            List<DiaDiem> diaDiems = db.DiaDiems.Where(dd => dd.Xoa == false).ToList();
            if(!string.IsNullOrEmpty(timDD))
            {
                diaDiems = db.DiaDiems.Where(dd => dd.TenDD.Contains(timDD)).ToList();
            }
            return View(diaDiems);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(DiaDiem diaDiem)
        {
            diaDiem.Xoa = false;
            db.DiaDiems.Add(diaDiem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var diaDiem = db.DiaDiems.Find(id);
            return View(diaDiem);        
        }
        [HttpPost]
        public ActionResult Edit(DiaDiem diaDiem)
        {
            db.DiaDiems.Attach(diaDiem);
            db.Entry(diaDiem).Property(model=>model.TenDD).IsModified = true;
            db.Entry(diaDiem).Property(model=>model.CapDD).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var diaDiem = db.DiaDiems.Find(id);
        //    if(diaDiem != null)
        //    {
        //        db.DiaDiems.Remove(diaDiem);
        //        db.SaveChanges();
        //        return Json(new { success=true });
        //    }
        //    return Json(new { success=false });
        //}
        public ActionResult Delete(int id)
        {
            var diaDiem = db.DiaDiems.Find(id);
            if (diaDiem == null)
            {
                return HttpNotFound();
            }
            diaDiem.Xoa = true;
            db.Entry(diaDiem).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}