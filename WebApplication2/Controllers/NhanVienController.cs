using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class NhanVienController : Controller
    {
        //KhÔng nên sử dụng chung tài khoản SQL để query
        private PasGoEntities2 db = new PasGoEntities2();
        // GET: NhanVien
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["level"]) != 99)
            {
                TempData["Failed"] = "Không có quyền truy cập!";
                return RedirectToAction("Index", "Admin");
            }
            var phonebox = Request.Form["phonebox"];
            var namebox = Request.Form["namebox"];
            var level = Convert.ToInt32( Request.Form["level"]);
            //Danh sách nhân viên truy cập vào DB
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            List<StaffList_Result> result = db.StaffSearch(phonebox, namebox, level).ToList();
            
            if (result.Count() < 1)
                TempData["Failed"] = "Không tìm thấy nhân viên";
            TempData["Success"] = "Trả về " + result.Count() + " bản ghi.";
            return View(result);
        }

        public ActionResult SetLevel()
        {
            var phonenumber = Request.Form["phonenumber"];
            var level = Convert.ToInt32( Request.Form["level"]);
            //result trả về giá trị int chứ không phải List
            System.Diagnostics.Trace.WriteLine("result: " + phonenumber + " / " + level.ToString());
            var result = db.StaffSetLevels(phonenumber, level).ToList().ElementAt(0);
            if (result == 0 || result == 2)
                TempData["Failed"] = "Cập nhật Level cho tài khoản " + phonenumber+" thất bại.";
            else
                TempData["Success"] = "Cập nhật Level cho tài khoản " + phonenumber + " thành công.";
            return RedirectToAction("Index", "NhanVien");
        }
    }
}