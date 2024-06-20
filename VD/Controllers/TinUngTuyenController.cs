using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Controllers
{
    public class TinUngTuyenController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public TinUngTuyenController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: TinUngTuyen
        public ActionResult Index_TUT(int id, string timTUT)
        {
            var ungVien = db.UngViens.Find(id);
            var tinUngTuyens = from tut in db.TinUngTuyens
                                join uv in db.UngViens on tut.ID_UV equals uv.ID_UV
                                join tk in db.TaiKhoans on uv.ID_TK equals tk.ID_TK
                                where tut.ID_UV == uv.ID_UV && uv.ID_TK == id
                                select tut;
            if (!string.IsNullOrEmpty(timTUT))
            {
                tinUngTuyens = db.TinUngTuyens.Where(tut => tut.HoTen.Contains(timTUT));
            }
            return View(tinUngTuyens.Where(t => t.Xoa == false).ToList());
        }
        private void LoadViewBags()
        {
            ViewBag.UngViens = db.UngViens.ToList();
            ViewBag.DiaDiems = db.DiaDiems.ToList();
            ViewBag.NganhNghes = db.NganhNghes.ToList();
            ViewBag.LinhVucs = db.LinhVucs.ToList();
        }
        public ActionResult Add_TUT()
        {
            LoadViewBags();
            return View();
        }
        [HttpPost]
        public ActionResult Add_TUT(TinUngTuyen tinUngTuyen)
        {
            var uv = Session["UngVien"] as VD.Models.UngVien;
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            try
            {
                if (ModelState.IsValid)
                {
                    tinUngTuyen.Xoa = false;
                    tinUngTuyen.ID_UV = uv.ID_UV;
                    tinUngTuyen.ID_Admin = 3;
                    tinUngTuyen.TgDang = DateTime.Now;
                    tinUngTuyen.TgianCapNhatTT = DateTime.Now;
                    tinUngTuyen.TrangThaiTUT = "Chờ xác nhận";
                    db.TinUngTuyens.Add(tinUngTuyen);
                    db.SaveChanges();
                    return RedirectToAction("Index_TUT", "TinUngTuyen", new { id = taiKhoan.ID_TK });
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
            return View(tinUngTuyen);
        }
        public ActionResult Edit_TUT(int id)
        {
            var tinUngTuyen = db.TinUngTuyens.Find(id);
            LoadViewBags();
            return View(tinUngTuyen);
        }
        [HttpPost]
        public ActionResult Edit_TUT(TinUngTuyen tinUngTuyen)
        {
            var uv = Session["UngVien"] as VD.Models.UngVien;
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            db.TinUngTuyens.Attach(tinUngTuyen);
            db.Entry(tinUngTuyen).Property(model => model.HoTen).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.SDT).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.DOB).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.GioiTinh).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.Email).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.PhucLoi).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.MucLuong).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.CapBac).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.KinhNghiemLV).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.TrinhDoHocVan).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.KyNangChuyenMon).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.ID_DD).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.ID_NN).IsModified = true;
            db.Entry(tinUngTuyen).Property(model => model.ID_LV).IsModified = true;
            LoadViewBags();
            db.SaveChanges();
            return RedirectToAction("Index_TUT", "TinUngTuyen", new { id = taiKhoan.ID_TK });
        }
        public ActionResult Delete(int id)
        {
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            var tinUngTuyen = db.TinUngTuyens.Find(id);
            if (tinUngTuyen == null)
            {
                return HttpNotFound();
            }
            tinUngTuyen.Xoa = true;
            db.Entry(tinUngTuyen).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_TUT", "TinUngTuyen", new { id = taiKhoan.ID_TK });
        }
    }
}