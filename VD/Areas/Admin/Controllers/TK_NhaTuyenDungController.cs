using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Areas.Admin.Controllers
{
    public class TK_NhaTuyenDungController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public TK_NhaTuyenDungController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: Admin/TK_NhaTuyenDung
        public ActionResult Index(string timTK_NTD = null)
        {
            List<TaiKhoan> taiKhoans = db.TaiKhoans.Where(tk => db.NhaTuyenDungs.Any(ntd => ntd.ID_TK == tk.ID_TK)).Where(ntd => ntd.Xoa == false).ToList();
            if (!string.IsNullOrEmpty(timTK_NTD))
            {
                taiKhoans = db.TaiKhoans.Where(ntd => ntd.TenDN.Contains(timTK_NTD)).ToList();
            }
            return View(taiKhoans);
        }
        public ActionResult ChiTietTK_NTD(int id)
        {
            var nhaTuyenDung = db.NhaTuyenDungs.FirstOrDefault(u => u.ID_TK == id);
            if (nhaTuyenDung == null)
            {
                return HttpNotFound();
            }
            return View(nhaTuyenDung);
        }

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    using (var transaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            // Tìm tất cả các NhaTuyenDung có ID_TK tương ứng
        //            var nhaTuyenDungs = db.NhaTuyenDungs.Where(u => u.ID_TK == id).ToList();
        //            if (nhaTuyenDungs.Any())
        //            {
        //                db.NhaTuyenDungs.RemoveRange(nhaTuyenDungs);
        //            }

        //            // Tìm TaiKhoan cần xóa
        //            var taiKhoan = db.TaiKhoans.Find(id);
        //            if (taiKhoan != null)
        //            {
        //                db.TaiKhoans.Remove(taiKhoan);
        //            }

        //            // Lưu thay đổi
        //            db.SaveChanges();

        //            // Commit transaction
        //            transaction.Commit();
        //            return Json(new { success = true });
        //        }
        //        catch (Exception ex)
        //        {
        //            // Rollback transaction nếu có lỗi xảy ra
        //            transaction.Rollback();
        //            return Json(new { success = false, message = ex.Message });
        //        }
        //    }
        //}
        public ActionResult Delete(int id)
        {
            var nhaTuyenDung = db.TaiKhoans.Find(id);
            if (nhaTuyenDung == null)
            {
                return HttpNotFound();
            }
            nhaTuyenDung.Xoa = true;
            db.Entry(nhaTuyenDung).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}