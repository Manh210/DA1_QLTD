using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VD.Models;

namespace VD.Controllers
{
    public class BaiDang_TUTController : Controller
    {
        private readonly QLTD20232Entities1 db;
        public BaiDang_TUTController()
        {
            db = new QLTD20232Entities1();
        }
        // GET: BaiDang_TUT
        public ActionResult Index_BDTUT_UV(string timTUT, int? id_DD, int? id_NN, int? id_LV)
        {
            LoadViewBags();
            var tinUngTuyens = db.TinUngTuyens.AsQueryable();
            if (!string.IsNullOrEmpty(timTUT))
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.HoTen.Contains(timTUT));
            }
            if (id_DD.HasValue && id_DD.Value > 0)
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.ID_DD == id_DD);
            }
            if (id_NN.HasValue && id_NN.Value > 0)
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.ID_NN == id_NN);
            }
            if (id_LV.HasValue && id_LV.Value > 0)
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.ID_LV == id_LV);
            }
            //if (!string.IsNullOrEmpty(loaiViec))
            //{
            //    tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.LoaiViec == loaiViec);
            //}
            return View(tinUngTuyens.Where(t => t.Xoa == false).ToList());
        }
        public ActionResult Index_BDTUT_NTD(string timTUT, int? id_DD, int? id_NN, int? id_LV)
        {
            LoadViewBags();
            var tinUngTuyens = db.TinUngTuyens.AsQueryable();
            if (!string.IsNullOrEmpty(timTUT))
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.HoTen.Contains(timTUT));
            }
            if (id_DD.HasValue && id_DD.Value > 0)
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.ID_DD == id_DD);
            }
            if (id_NN.HasValue && id_NN.Value > 0)
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.ID_NN == id_NN);
            }
            if (id_LV.HasValue && id_LV.Value > 0)
            {
                tinUngTuyens = tinUngTuyens.Where(t => t.ID_LV == id_LV);
            }
            //if (!string.IsNullOrEmpty(loaiViec))
            //{
            //    tinTuyenDungs = tinTuyenDungs.Where(t => t.CongViec.LoaiViec == loaiViec);
            //}
            return View(tinUngTuyens.Where(t => t.Xoa == false).ToList());
        }
        private void LoadViewBags()
        {
            ViewBag.UngViens = db.UngViens.ToList();
            ViewBag.DiaDiems = db.DiaDiems.ToList();
            ViewBag.NganhNghes = db.NganhNghes.ToList();
            ViewBag.LinhVucs = db.LinhVucs.ToList();
        }
        public ActionResult ChiTietBDTUT_UV(int id)
        {
            var tinUngTuyen = db.TinUngTuyens.Find(id);
            LoadViewBags();
            return View(tinUngTuyen);
        }
        public ActionResult ChiTietBDTUT_NTD(int id)
        {
            var tinUngTuyen = db.TinUngTuyens.Find(id);
            LoadViewBags();
            return View(tinUngTuyen);
        }
    }
}