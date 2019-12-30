using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Globalization;

namespace WebApplication2.Controllers
{
    public class NguoiDungController : Controller
    {
        private PasGoEntities db = new PasGoEntities();

        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            var id = Request.Form["idbox"];
            if (id == "")
                id = null;
            var phone = Request.Form["phonebox"];
            var email = Request.Form["emailbox"];
            var name = Request.Form["namebox"];
            var locked = Convert.ToInt32( Request.Form["locked"]);
            var visitor = Convert.ToInt32(Request.Form["visitor"]);
            System.Diagnostics.Trace.WriteLine("result: " + id + " / " + phone + " / " + email + " / " + name + " / " + locked);
            var result = db.SimpleUser(id, phone, email, name, Convert.ToBoolean(locked), Convert.ToBoolean(visitor));
            var c = db.SimpleUser(id, phone, email, name, Convert.ToBoolean(locked), Convert.ToBoolean(visitor)).Count();
            if (c == 0)
                TempData["Failed"] = "Không có kết quả tìm kiếm phù hợp";
            else
                TempData["Success"] = "Tìm được " + c.ToString() + " kết quả.";

            return View(result.ToList());
            //return View(Tuple.Create(result.ToList(), id, phone, email, name, locked));
        }

        public ActionResult Details(string PasgoID, int type)
        {
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            FullInfoUser_Result result = db.FullInfoUser(Convert.ToInt32(PasgoID), Convert.ToBoolean(type)).ElementAt(0);
            List<SimpleHistory_Result> hresult = db.SimpleHistory(Convert.ToInt32(PasgoID), Convert.ToBoolean(type)).ToList();
            return View(Tuple.Create(result, hresult, type));
        }
        public ActionResult History(int PasgoID, int type)
        {
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            //Lỗi: Nếu ảnh ko để type=99 -> bị loại -> thiếu lịch sử
            var x = Request.Form["from"];
            var y = Request.Form["to"];
            DateTime today = DateTime.Now;
            DateTime defaultmin = DateTime.ParseExact("01-01-2001", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime from = defaultmin;
            DateTime to = DateTime.Now;
            if (x != "" && x != null)
                from = DateTime.ParseExact(x, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (y != "" && y != null)
                to = DateTime.ParseExact(y, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (to > today)
                to = today;
            if (from > today || from > to)
                TempData["Failed"] = "Nhập thông tin ngày tháng sai";
            System.Diagnostics.Trace.WriteLine("from: " + from + " /to: " + to);
            List<FullInfoHistory_Result> result = db.FullInfoHistory(PasgoID, type, from, to).ToList();
            if (result.Count() == 0)
            {
                TempData["Failed"] = "Lịch sử đặt bàn trống";
                return View(Tuple.Create(result, PasgoID, type));
            }
            if (result.Count() > 0 && from > defaultmin)
            {
                TempData["Success"] = "Hiển thị " + result.Count().ToString() + " bản ghi lịch sử đặt chỗ từ " + from.ToString("dd-MMM-yyyy") + " đến " + to.ToString("dd-MMM-yyyy");
                //return View(Tuple.Create(result, PasgoID, type, from, to));
            }
            else
            {
                TempData["Success"] = "Hiển thị " + result.Count().ToString() + " bản ghi lịch sử đặt chỗ từ " + result.ElementAt(result.Count() - 1).CallTime.ToString("dd-MMM-yyyy") + " đến " + result.ElementAt(0).CallTime.ToString("dd-MMM-yyyy");
                //return View(Tuple.Create(result, PasgoID, type, result.ElementAt(result.Count() - 1).CallTime, result.ElementAt(0).CallTime));
            }
            return View(Tuple.Create(result, PasgoID, type));
        }

        public ActionResult Unlock(int PasgoID, int usertype)
        {
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            var result = db.LockUnlock(PasgoID, 1, "a", 1).ToList().ElementAt(0);
            if (Convert.ToInt32(result) == 1)
                TempData["Success"] = "Mở khóa tài khoản thành công";
            else
                TempData["Failed"] = "Lỗi, xin thử lại!";
            return RedirectToAction("Details", "NguoiDung", new { PasgoID = PasgoID.ToString(), type = usertype });
        }

        public ActionResult Lock()
        {
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            string id = Request.Form["id"];
            string type = Request.Form["type"];
            string usertype = Request.Form["usertype"];
            int howlong = 0;
            if (Request.Form["howlong"] == "")
            {
                TempData["Failed"] = "Chưa nhập thời gian khóa!";
                return RedirectToAction("Details", "NguoiDung", new { PasgoID = id, type = usertype });
            }
            else
                howlong = Convert.ToInt32(Request.Form["howlong"]);
            var result = db.LockUnlock(Convert.ToInt32(id), 0, type, howlong).ToList().ElementAt(0);
            if (Convert.ToInt32(result) == 1 )
                TempData["Success"] = "Tài khoản bị khóa thành công";
            else
                TempData["Failed"] = "Lỗi, xin thử lại!";
            return RedirectToAction("Details", "NguoiDung", new { PasgoID = id, type = usertype });
        }
    }
}