using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ManageAccountController : Controller
    {
        private PasGoEntities db = new PasGoEntities();

        private bool CheckUserAuthorize()
        {
            string phonenumber;
            if (Session["PhoneNumber"] == null)
                return false;
            phonenumber = Session["PhoneNumber"].ToString();
            if (db.PasgoUsers.Where(x => x.PhoneNumber == phonenumber).FirstOrDefault() != null)
                return true;
            return false;
        }
        private static string CreatSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytearr = new byte[32];
            rng.GetBytes(bytearr);

            return Convert.ToBase64String(bytearr);
        }

        [Obsolete]
        private static string Hashed(string password, string salt)
        {
            string passwordSalt = String.Concat(password, salt);
            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(passwordSalt, "sha1");

            return hashedPassword;
        }
        
        // GET: ManageAccount
        public ActionResult Details()
        {
            if (Session["PasgoID"] != null)
            {
                    //List<PasgoUser> userModel = db.PasgoUsers.ToList();
                    System.Diagnostics.Trace.WriteLine(Session["PhoneNumber"].ToString());
                    GetUserInforSP_Result info = db.GetUserInforSP(Session["PhoneNumber"].ToString()).ElementAt(0);
                    return View(info);
            }
            else
                return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Models.GetUserInforSP_Result model)
        {  
            
            if (ModelState.IsValid)
            {
                var result = db.UpdateUserInfo(Convert.ToInt32(Session["PasgoID"]), model.FullName, model.Email, model.DOB, model.Gender_).ToList().ElementAt(0);
                switch (Convert.ToInt32(result))
                {
                    case 0:
                        TempData["Failed"] = "Cập nhật thông tin người dùng thất bại.";
                        break;
                    case 2:
                        TempData["Failed"] = "Cập nhật thông tin người dùng thất bại. Người dùng không tồn tại.";
                        break;
                    case 3:
                        TempData["Failed"] = "Cập nhật thông tin người dùng thất bại. Email đã được sử dụng.";
                        break;
                    default:
                        TempData["Success"] = "Cập nhật thông tin người dùng thành công.";
                        break;
                }
                System.Diagnostics.Trace.WriteLine("result :" + result);
                System.Diagnostics.Trace.WriteLine("id " + Convert.ToInt32(Session["PasgoID"]) + "/name " + model.FullName + "/dob " + model.DOB.ToString());
                System.Diagnostics.Trace.WriteLine( "/dob " + model.DOB.ToString());
            }
            else
                TempData["Failed"] = "Cập nhật thông tin người dùng thất bại. Thông tin nhập vào không đúng định dạng.";
            GetUserInforSP_Result info = db.GetUserInforSP(Session["PhoneNumber"].ToString()).ElementAt(0);
            return View(info);
        }

        public ActionResult ChangePassword()
        {
            if (Session["PasgoID"] != null)
                return View();
            return View("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult ChangePassword(string Phone, string PasgoID)
        {
            string oldpw = Request.Form["old"];
            string newpw = Request.Form["new"];
            string retypenew = Request.Form["retypenew"];
            string salt = db.Salt(Phone).ToList().ElementAt(0).ToString();
            string oldhash = Hashed(oldpw, salt);

            if (oldpw =="" || newpw == null || retypenew == null)
            {
                @TempData["Failed"] = "Mật khẩu không được để trống.";
                return View();
            }  

            if (newpw != retypenew)
            {
                @TempData["Failed"] = "Mật khẩu không giống nhau."; 
                return View();
            }

            string newsalt = CreatSalt();
            string newhash = Hashed(newpw, newsalt);
            var result = db.UpdatePassword(Convert.ToInt32(PasgoID), oldhash, newsalt, newhash).ToList().ElementAt(0);

            switch (Convert.ToInt32(result))
            {
                case 0:
                    @TempData["Failed"] = "Thay đổi mật khẩu thất bại.";
                    return View();
                case 2:
                    @TempData["Failed"] = "Mật khẩu không chính xác.";
                    return View();
                default:
                    @TempData["Success"] = "Thay đổi mật khẩu thành công.";
                    return View();
            }
        }
        public ActionResult History()
        {
            var PasgoID = Session["PasgoID"];
            if (PasgoID == null)
                return View("Index", "Home");
            var result = db.SimpleHistory(Convert.ToInt32(PasgoID), false).ToList();
            List<SimpleHistory_Result> moi = new List<SimpleHistory_Result>();
            List<SimpleHistory_Result> cu = new List<SimpleHistory_Result>();
            foreach (var item in result)
            {
                if (item.TrangThai == "Đang chờ xác nhận")
                    moi.Add(item);
                else
                    cu.Add(item);
            }
            return View(Tuple.Create( moi.ToList(), cu.ToList()));
        }

        public ActionResult Conversation()
        {
            var id = Convert.ToInt32( Session["PasgoID"]);
            List<Conversation> result = db.Conversations.Where(x => x.IdUser == id).ToList();
            return View();
        }

        public ActionResult CreatConversation()
        {
            var iduser = Convert.ToInt32(Session["PasgoID"]);
            if(System.DateTime.Now< System.DateTime.Today.AddHours(8) || System.DateTime.Now > System.DateTime.Today.AddHours(20)){
                TempData["Failed"] = "Hệ thống CSKH hiện đang nghỉ, xin vui lòng liên hệ thời gian làm việc 8:00 - 20:00.";
                return RedirectToAction("Chat", "ManageAccount");
            }
            if (CheckUserAuthorize() == true)
            {
                //add exception
                var result = db.AddConversation(iduser).ToList().ElementAt(0);
                if (Convert.ToInt32(result) != 0) {
                    TempData["Success"] = "Hội thoại đã được thiết lập!";
                    db.SaveMessage(Convert.ToInt32(result), false, "Xin chào, tôi có thể giúp gì cho bạn?");
                }
                else
                    TempData["Failed"] = "Hệ thống quá tải, xin vui lòng thử lại sau!";
                return RedirectToAction("Chat", "ManageAccount");
            }
            return View("Index", "Home");
        }

        public ActionResult Chat()
        {
            if (Session["PasgoID"] == null)
                return RedirectToAction("Index", "Home");
            var id = Convert.ToInt32(Session["PasgoID"]);
            List<Conversation> result = db.Conversations.Where(x => x.IdUser == id).ToList();
            return View(result);
        }
    }
}