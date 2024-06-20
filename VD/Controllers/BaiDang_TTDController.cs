using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Controllers
{
    public class BaiDang_TTDController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public BaiDang_TTDController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: BaiDang_TTD
        public ActionResult Index_BDTTD(string timTTD, int? id_CV, int? id_DD, int? id_NN, int? id_LV, string loaiViec)
        {
            LoadViewBags();
            var tinTuyenDungs = db.TinTuyenDungs.AsQueryable();
            if (!string.IsNullOrEmpty(timTTD))
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.TieuDe.Contains(timTTD));
            }
            //if (id_CV.HasValue && id_CV.Value > 0)
            //{
            //    tinTuyenDungs = tinTuyenDungs.Where(t => t.ID_CV == id_CV);
            //}
            if (id_DD.HasValue && id_DD.Value > 0)
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.ID_DD == id_DD);
            }
            if (id_NN.HasValue && id_NN.Value > 0)
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.ID_NN == id_NN);
            }
            if (id_LV.HasValue && id_LV.Value > 0)
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.ID_LV == id_LV);
            }
            if (!string.IsNullOrEmpty(loaiViec))
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.LoaiViec == loaiViec);
            }
            return View(tinTuyenDungs.Where(t => t.Xoa == false).ToList());
        }
        public ActionResult Index_BDTTD_UV(string timTTD, int? id_CV, int? id_DD, int? id_NN, int? id_LV, string loaiViec)
        {
            LoadViewBags();
            var tinTuyenDungs = db.TinTuyenDungs.AsQueryable();
            if (!string.IsNullOrEmpty(timTTD))
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.TieuDe.Contains(timTTD));
            }
            //if (id_CV.HasValue && id_CV.Value > 0)
            //{
            //    tinTuyenDungs = tinTuyenDungs.Where(t => t.ID_CV == id_CV);
            //}
            if (id_DD.HasValue && id_DD.Value > 0)
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.ID_DD == id_DD);
            }
            if (id_NN.HasValue && id_NN.Value > 0)
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.ID_NN == id_NN);
            }
            if (id_LV.HasValue && id_LV.Value > 0)
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.ID_LV == id_LV);
            }
            if (!string.IsNullOrEmpty(loaiViec))
            {
                tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.LoaiViec == loaiViec);
            }
            return View(tinTuyenDungs.Where(t => t.Xoa == false).ToList());
        }
        private void LoadViewBags()
        {
            ViewBag.NhaTuyenDungs = db.NhaTuyenDungs.ToList();
            ViewBag.CongViecs = db.CongViecs.ToList();
            ViewBag.DiaDiems = db.DiaDiems.ToList();
            ViewBag.NganhNghes = db.NganhNghes.ToList();
            ViewBag.LinhVucs = db.LinhVucs.ToList();
        }
        public ActionResult ChiTietBDTTD(int id)
        {
            var tinTuyenDung = db.TinTuyenDungs.Find(id);
            LoadViewBags();
            return View(tinTuyenDung);
        }
        public ActionResult ChiTietBDTTD_UV(int id)
        {
            var tinTuyenDung = db.TinTuyenDungs.Find(id);
            LoadViewBags();
            return View(tinTuyenDung);
        }
        /*[HttpPost]
        public ActionResult UngTuyen(int iD_CV, int iD_TTD)
        {
            //var tinTuyenDung = Session["TinUngTuyen"] as VD.Models.TinTuyenDung;
            var ungVien = Session["UngVien"] as VD.Models.UngVien;
            var congViec = db.CongViecs.Find(iD_CV);
            if (ungVien != null && congViec != null)
            {
                UngTuyen ungTuyen = new UngTuyen
                {
                    ID_UV = ungVien.ID_UV,
                    ID_CV = congViec.ID_CV,
                    TrangThaiUT = "Chờ xác nhận",
                    TgianCapNhatTT = DateTime.Now
                };
                db.UngTuyens.Add(ungTuyen);
                db.SaveChanges();
                TempData["Message"] = "Ứng tuyển thành công";
                return RedirectToAction("ChiTietBDTTD_UV", "BaiDang_TTD", new { id = iD_TTD });
            }
            return RedirectToAction("ChiTietBDTTD_UV", "BaiDang_TTD", new { id = iD_TTD });
        }*/
        [HttpPost]
        public JsonResult UngTuyen(int iD_CV, int iD_TTD)
        {
            var ungVien = Session["UngVien"] as VD.Models.UngVien;
            var tinTuyenDung = db.TinTuyenDungs.Find(iD_TTD);
            var congViec = db.CongViecs.Find(iD_CV);
            if (ungVien != null && congViec != null)
            {
                UngTuyen ungTuyen = new UngTuyen
                {
                    Xoa = false,
                    ID_UV = ungVien.ID_UV,
                    ID_CV = congViec.ID_CV,
                    ID_TTD = tinTuyenDung.ID_TTD,
                    TrangThaiUT = "Chờ xác nhận",
                    TgianCapNhatTT = DateTime.Now
                };
                db.UngTuyens.Add(ungTuyen);
                db.SaveChanges();
                return Json(new { success = true, message = "Ứng tuyển thành công" });
            }
            return Json(new { success = false, message = "Ứng tuyển thất bại" });
        }
    }
}