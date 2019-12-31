using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private PasGoEntities db = new PasGoEntities();
        public ActionResult Index() 
        {
            return View();
        }

        public ActionResult SlideShow()
        {
            System.Diagnostics.Trace.WriteLine("QuanID");
            System.Diagnostics.Trace.WriteLine(Request.Form["fQuanId"]);
            var slide = db.imgSlide();
            List<imgSlide_Result> result = new List<imgSlide_Result>();
            foreach (var rs in slide.ToList())
            {
                result.Add(rs);
            }
            var x = db.UpdateAvatar(1, "asdasdas");
            System.Diagnostics.Trace.WriteLine(x.ToList().ElementAt(0));
            return PartialView("SlideShow", result.ToArray());
        }

        public ActionResult BoSuuTap(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var bst = db.MenuCatalogue(city);
            List<Menu> result = new List<Menu>();
            foreach(var rs in bst.ToList())
            {
                result.Add(rs);
            }

            return PartialView("BoSuuTap", result.ToArray());
        }

        public ActionResult AnUong(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var anuong = db.AnUongSP(city);
            List<AnUongSP_Result> result = new List<AnUongSP_Result>();
            foreach(var rs in anuong.ToList())
            {
                result.Add(rs);
            }
            return PartialView("AnUong", result.ToArray());
        }

        public ActionResult UyTin(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var uytin = db.UyTinSP(city);
            List<AnUongSP_Result> result = new List<AnUongSP_Result>();
            foreach(var rs in uytin.ToList())
            {
                //System.Diagnostics.Trace.WriteLine(rs.imgLink);
                //System.Diagnostics.Trace.WriteLine(rs.RName);
                result.Add(rs);
            }
            return PartialView("UyTin", result.ToArray());
        }

        public ActionResult UuDai(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var uudai = db.AnUongSP(city);
            List<AnUongSP_Result> result = new List<AnUongSP_Result>();
            foreach (var rs in uudai.ToList())
            {
                result.Add(rs);
            }
            return PartialView("UuDai", result.ToArray());
        }

        public ActionResult MoiNhat(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var moinhat = db.AnUongSP(city);
            List<AnUongSP_Result> result = new List<AnUongSP_Result>();
            foreach (var rs in moinhat.ToList())
            {
                result.Add(rs);
            }
            return PartialView("MoiNhat", result.ToArray());
        }

        public ActionResult KhamPha(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var khampha = db.KhamPhaSP(city);
            List<KhamPhaBv> result = new List<KhamPhaBv>();
            foreach(var rs in khampha.ToList())
            {
                result.Add(rs);
            }
            return PartialView("KhamPha", result.ToArray());
        }
        public ActionResult ChuoiThuongHieu(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var chuoi = db.ChuoiThuongHieuSP(city);
            List<ThuongHieuChuoi> result = new List<ThuongHieuChuoi>();
            foreach(var rs in chuoi.ToList())
            {
                result.Add(rs);
            }
            return PartialView("ChuoiThuongHieu", result.ToArray());
        }

        public ActionResult BlogPasgo(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var blog = db.BaiVietSP(city, 1);
            List<BaiVietSP_Result> result = new List<BaiVietSP_Result>();
            foreach(var rs in blog.ToList())
            {
                result.Add(rs);
            }
            return PartialView("BlogPasgo", result.ToArray());
        }
        public ActionResult TinTuc(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var blog = db.BaiVietSP(city, 2);
            List<BaiVietSP_Result> result = new List<BaiVietSP_Result>();
            foreach (var rs in blog.ToList())
            {
                result.Add(rs);
            }
            return PartialView("TinTuc", result.ToArray());
        }
        public ActionResult PasgoTv(int? city)
        {
            if (city == null)
                return RedirectToAction("Index", "Home");
            var video = db.PasgotvSP(city);
            List<PasgoTV> result = new List<PasgoTV>();
            foreach(var rs in video.ToList())
            {
                result.Add(rs);
            }
            return PartialView("PasgoTv", result.ToArray());
        }

        public ActionResult KhachHang()
        {
            var kh = db.KhachhangSP();
            List<KhachHang> result = new List<KhachHang>();
            foreach(var rs in kh.ToList())
            {
                //System.Diagnostics.Trace.WriteLine("Khach Hang:");
                //System.Diagnostics.Trace.WriteLine(rs.Name);
                //System.Diagnostics.Trace.WriteLine(rs.Job);
                result.Add(rs);
            }
            return PartialView("KhachHang", result.ToArray());
        }

        public ActionResult DoiTac()
        {
            var dt = db.DoiTacSP();
            List<DoiTacSP_Result> result = new List<DoiTacSP_Result>();
            foreach(var rs in dt.ToList())
            {
                result.Add(rs);
            }
            return PartialView("DoiTac", result.ToArray());
        }
        public ActionResult Login()
        {
            return PartialView("Login", "Account");
        }
        public ActionResult DangKy()
        {
            return View();
        }

        public ActionResult hochiminh()
        {
            return View();
        }
        public ActionResult danang()
        {
            return View();
        }
        public ActionResult khanhhoa()
        {
            return View();
        }
    }
}