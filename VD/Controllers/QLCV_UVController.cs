using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Controllers
{
    public class QLCV_UVController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public QLCV_UVController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: QLCV_UV
        public ActionResult ListCV(int id, string timCV)
        {
            var ungVien = db.UngViens.Find(id);
            //var ungTuyens = from ut in db.UngTuyens
            //                join uv in db.UngViens on ut.ID_UV equals uv.ID_UV
            //                join cv in db.CongViecs on ut.ID_CV equals cv.ID_CV
            //                where ut.ID_UV == uv.ID_UV && uv.ID_UV == id
            //                select ut;
            var ungTuyens = from ut in db.UngTuyens
                            join uv in db.UngViens on ut.ID_UV equals uv.ID_UV
                            join tk in db.TaiKhoans on uv.ID_TK equals tk.ID_TK
                            where ut.ID_UV == uv.ID_UV && uv.ID_TK == id
                            select ut;
            if (!string.IsNullOrEmpty(timCV))
            {
                ungTuyens = db.UngTuyens.Where(cv => cv.CongViec.TenCV.Contains(timCV));
            }
            return View(ungTuyens.Where(t => t.Xoa == false).ToList());
        }
        public ActionResult ChiTietCV(int id)
        {
            var ungTuyen = db.UngTuyens.Find(id);
            //var ungVien = db.UngViens.FirstOrDefault(u => u.ID_TK == id);
            return View(ungTuyen);
        }
        public ActionResult Delete_CV(int id)
        {
            var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            var ungTuyen = db.UngTuyens.Find(id);
            if (ungTuyen == null)
            {
                return HttpNotFound();
            }
            ungTuyen.Xoa = true;
            db.Entry(ungTuyen).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListCV", "QLCV_UV", new { id = taiKhoan.ID_TK });
        }
        public ActionResult UngTuyenCV(int id)
        {
            var ungVien = db.UngViens.Find(id);
            //var ungTuyens = from ut in db.UngTuyens
            //                join cv in db.CongViecs on ut.ID_CV equals cv.ID_CV
            //                join uv in db.UngViens on ut.ID_UV equals uv.ID_UV
            //                where ut.ID_CV == cv.ID_CV && cv.ID_CV == id
            //                select ut;
            var ungTuyens = from ut in db.UngTuyens
                              join uv in db.UngViens on ut.ID_UV equals uv.ID_UV
                              join cv in db.CongViecs on ut.ID_CV equals cv.ID_CV
                              where ut.ID_UV == uv.ID_UV && uv.ID_UV == id
                              select ut;
            return View(ungTuyens.Where(t => t.Xoa == false).ToList());
        }
        public ActionResult Delete_UT(int id)
        {
            //var taiKhoan = Session["TaiKhoan"] as VD.Models.TaiKhoan;
            var ungVien = Session["UngVien"] as VD.Models.UngVien;
            var ungTuyen = db.UngTuyens.Find(id);
            if (ungTuyen == null)
            {
                return HttpNotFound();
            }
            ungTuyen.Xoa = true;
            db.Entry(ungTuyen).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("UngTuyenCV", "QLCV_UV", new { id = ungVien.ID_UV });
        }
    }
}