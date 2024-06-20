using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace VD.Controllers
{
    public class QLCVController : Controller
    {
        // GET: QLCV
        //QLTD20232Entities1 db = new QLTD20232Entities1();
        private readonly QLTD20232Entities1 db;
        public QLCVController()
        {
            db = new QLTD20232Entities1();
        }
        public ActionResult Index_CV(int id, string timCV)
        {
            var nhaTuyenDung = db.NhaTuyenDungs.Find(id);
            var congViecs = from cv in db.CongViecs
                            join ntd in db.NhaTuyenDungs on cv.ID_NTD equals ntd.ID_NTD
                            join tk in db.TaiKhoans on ntd.ID_TK equals tk.ID_TK
                            where cv.ID_NTD == ntd.ID_NTD && ntd.ID_TK == id
                            select cv;
            if (!string.IsNullOrEmpty(timCV))
            {
                congViecs = db.CongViecs.Where(cv => cv.TenCV.Contains(timCV));
            }
            return View(congViecs.Where(t => t.Xoa == false).ToList());
        }
        public ActionResult Add_CV()
        {
            LoadViewBags();
            return View();
        }
        [HttpPost]
        public ActionResult Add_CV(CongViec congViec)
        {
            var ntd = Session["NhaTuyenDung"] as VD.Models.NhaTuyenDung;
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            try
            {
                if (ModelState.IsValid)
                {
                    congViec.Xoa = false;
                    congViec.ID_NTD = ntd.ID_NTD;
                    congViec.ID_Admin = 3;
                    db.CongViecs.Add(congViec);
                    db.SaveChanges();
                    return RedirectToAction("Index_CV", "QLCV", new { id = taiKhoan.ID_TK });
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                        // Log the error details
                        System.Diagnostics.Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log general exceptions
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            }
            LoadViewBags();
            return View(congViec);
        }
        private void LoadViewBags()
        {
            ViewBag.NganhNghes = db.NganhNghes.ToList();
            ViewBag.NhaTuyenDungs = db.NhaTuyenDungs.ToList();
            ViewBag.DiaDiems = db.DiaDiems.ToList();
            ViewBag.LinhVucs = db.LinhVucs.ToList();
        }
        public ActionResult Edit_CV(int id)
        {
            var congViec = db.CongViecs.Find(id);
            LoadViewBags();
            return View(congViec);
        }
        [HttpPost]
        public ActionResult Edit_CV(CongViec congViec)
        {
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            db.CongViecs.Attach(congViec);
            db.Entry(congViec).Property(model => model.TenCV).IsModified = true;
            db.Entry(congViec).Property(model => model.MoTaCV).IsModified = true;
            db.Entry(congViec).Property(model => model.LoaiViec).IsModified = true;
            db.Entry(congViec).Property(model => model.ID_NN).IsModified = true;
            db.Entry(congViec).Property(model => model.ID_DD).IsModified = true;
            db.Entry(congViec).Property(model => model.ID_LV).IsModified = true;
            //db.Entry(congViec).Property(model => model.ID_NTD).IsModified = true;
            LoadViewBags();
            db.SaveChanges();
            return RedirectToAction("Index_CV", "QLCV", new { id = taiKhoan.ID_TK });
        }
        //[HttpPost]
        //public ActionResult Delete_CV(int id)
        //{
        //    var congViec = db.CongViecs.Find(id);
        //    if (congViec != null)
        //    {
        //        db.CongViecs.Remove(congViec);
        //        db.SaveChanges();
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });
        //}
        public ActionResult Delete_CV(int id)
        {
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            var congViec = db.CongViecs.Find(id);
            if (congViec == null)
            {
                return HttpNotFound();
            }
            congViec.Xoa = true;
            db.Entry(congViec).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_CV", "QLCV", new { id = taiKhoan.ID_TK });
        }
        public ActionResult ListUV(int id, string timUV)
        {
            var congViec = db.CongViecs.Find(id);
            var ungTuyens = from ut in db.UngTuyens
                            join cv in db.CongViecs on ut.ID_CV equals cv.ID_CV
                            join uv in db.UngViens on ut.ID_UV equals uv.ID_UV
                            where ut.ID_CV == cv.ID_CV && cv.ID_CV == id
                            select ut;
            if (!string.IsNullOrEmpty(timUV))
            {
                ungTuyens = db.UngTuyens.Where(cv => cv.UngVien.HoTen.Contains(timUV));
            }
            return View(ungTuyens.Where(t => t.Xoa == false).ToList());
        }
        //[HttpPost]
        //public ActionResult Delete_UT(int id)
        //{
        //    var ungTuyen = db.UngTuyens.Find(id);
        //    if (ungTuyen != null)
        //    {
        //        db.UngTuyens.Remove(ungTuyen);
        //        db.SaveChanges();
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false });
        //}
        public ActionResult ChiTiet(int id)
        {
            var ungTuyen = db.UngTuyens.Find(id);
            //var ungVien = db.UngViens.FirstOrDefault(u => u.ID_TK == id);
            return View(ungTuyen);
        }
        public ActionResult ListUT(int id)
        {
            var congViec = db.CongViecs.Find(id);
            var ungTuyens = from ut in db.UngTuyens
                            join cv in db.CongViecs on ut.ID_CV equals cv.ID_CV
                            join uv in db.UngViens on ut.ID_UV equals uv.ID_UV
                            where ut.ID_CV == cv.ID_CV && cv.ID_CV == id
                            select ut;
            return View(ungTuyens.Where(t => t.Xoa == false).ToList());
        }
        public ActionResult XuLyUT(int id)
        {
            var ungTuyen = db.UngTuyens.Find(id);
            return View(ungTuyen);
        }
        [HttpPost]
        public ActionResult XuLyUT(UngTuyen ungTuyen, int iD_CV, int iD_UV)
        {
            var congViec = Session["CongViec"] as VD.Models.CongViec;
            ungTuyen.ID_UV = iD_UV;
            db.UngTuyens.Attach(ungTuyen);
            db.Entry(ungTuyen).Property(model => model.TrangThaiUT).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("ListUV", "QLCV", new { id = iD_CV });
        }
    }
}