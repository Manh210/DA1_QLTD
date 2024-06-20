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
    public class TinTuyenDungController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public TinTuyenDungController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: TinTuyenDung
        public ActionResult Index_TTD(int id, string timTTD)
        {
            var nhaTuyenDung = db.NhaTuyenDungs.Find(id);
            var tinTuyenDungs = from ttd in db.TinTuyenDungs
                            join ntd in db.NhaTuyenDungs on ttd.ID_NTD equals ntd.ID_NTD
                            join tk in db.TaiKhoans on ntd.ID_TK equals tk.ID_TK
                            where ttd.ID_NTD == ntd.ID_NTD && ntd.ID_TK == id
                            select ttd;
            if (!string.IsNullOrEmpty(timTTD))
            {
                tinTuyenDungs = db.TinTuyenDungs.Where(ttd => ttd.TieuDe.Contains(timTTD));
            }
            return View(tinTuyenDungs.Where(t => t.Xoa == false).ToList());
        }
        private void LoadViewBags()
        {
            ViewBag.NhaTuyenDungs = db.NhaTuyenDungs.ToList();
            ViewBag.CongViecs = db.CongViecs.ToList();
        }
        public ActionResult Add_TTD()
        {
            LoadViewBags();
            return View();
        }
        [HttpPost]
        public ActionResult Add_TTD(TinTuyenDung tinTuyenDung)
        {
            var ntd = Session["NhaTuyenDung"] as VD.Models.NhaTuyenDung;
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            try
            {
                if (ModelState.IsValid)
                {
                    tinTuyenDung.Xoa = false;
                    tinTuyenDung.ID_NTD = ntd.ID_NTD;
                    tinTuyenDung.ID_Admin = 3;
                    tinTuyenDung.TgDang = DateTime.Now;
                    tinTuyenDung.TgianCapNhatTT = DateTime.Now;
                    tinTuyenDung.TrangThaiTTD = "Chờ xác nhận";
                    db.TinTuyenDungs.Add(tinTuyenDung);
                    db.SaveChanges();
                    return RedirectToAction("Index_TTD", "TinTuyenDung", new { id = taiKhoan.ID_TK });
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
            return View(tinTuyenDung);
        }
        public ActionResult Edit_TTD(int id)
        {
            var tinTuyenDung = db.TinTuyenDungs.Find(id);
            LoadViewBags();
            return View(tinTuyenDung);
        }
        [HttpPost]
        public ActionResult Edit_TTD(TinTuyenDung tinTuyenDung)
        {
            var ntd = Session["NhaTuyenDung"] as VD.Models.NhaTuyenDung;
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            db.TinTuyenDungs.Attach(tinTuyenDung);
            db.Entry(tinTuyenDung).Property(model => model.TieuDe).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.YeuCau).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.TgLam).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.HanUT).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.SoLuongTD).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.PhucLoi).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.MucLuong).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.CapBac).IsModified = true;
            db.Entry(tinTuyenDung).Property(model => model.ID_CV).IsModified = true;
            LoadViewBags();
            db.SaveChanges();
            return RedirectToAction("Index_TTD", "TinTuyenDung", new { id = taiKhoan.ID_TK });
        }
        //public ActionResult Delete(int id)
        //{
        //    var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
        //    var tinTuyenDung = db.TinTuyenDungs.Find(id);
        //    if (tinTuyenDung == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    tinTuyenDung.Xoa = true;
        //    db.Entry(tinTuyenDung).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index_TTD", "TinTuyenDung", new { id = taiKhoan.ID_TK });
        //}

        public ActionResult Delete_TTD(int id)
        {
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Tìm tất cả các NhaTuyenDung có ID_TK tương ứng
                    var ungTuyens = db.UngTuyens.Where(u => u.ID_TTD == id).ToList();
                    if (ungTuyens.Any())
                    {
                        db.UngTuyens.RemoveRange(ungTuyens);
                    }

                    // Tìm TaiKhoan cần xóa
                    var tinTuyenDung = db.TinTuyenDungs.Find(id);
                    if (tinTuyenDung != null)
                    {
                        db.TinTuyenDungs.Remove(tinTuyenDung);
                    }

                    // Lưu thay đổi
                    db.SaveChanges();

                    // Commit transaction
                    transaction.Commit();
                    return RedirectToAction("Index_TTD", "TinTuyenDung", new { id = taiKhoan.ID_TK });
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    transaction.Rollback();
                    return RedirectToAction("Index_TTD", "TinTuyenDung", new { id = taiKhoan.ID_TK });
                }
                //    var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
                //var ttd = db.TinTuyenDungs.Find(id);
                //if (ttd == null)
                //{
                //    return HttpNotFound();
                //}
                //db.TinTuyenDungs.Remove(ttd);
                //db.SaveChanges();
                //return RedirectToAction("Index_TTD", "TinTuyenDung", new { id = taiKhoan.ID_TK });
            }
        }
    }
}