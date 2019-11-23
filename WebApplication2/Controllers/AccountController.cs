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
    public class AccountController : Controller
    {
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
            //Lỗi thời?
            string passwordSalt = String.Concat(password, salt);
            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(passwordSalt, "sha1");

            return hashedPassword;
        }
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {   
            if(Session["PasgoID"]==null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [Obsolete]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.PasgoUser userModel)
        {
            using(PasGoEntities2 db = new PasGoEntities2())
            {
                if (db.Salt(userModel.PhoneNumber).Count() == 0)
                {
                    TempData["Failed"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
                string salt = db.Salt(userModel.PhoneNumber).ElementAt(0).ToString();
                string hash = Hashed(userModel.Password, salt);            
                if(db.Login(userModel.PhoneNumber, hash).Count() == 0)
                {
                    TempData["Failed"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View("Login");
                }
                else
                {
                    var userDetails = db.Login(userModel.PhoneNumber, hash).ElementAt(0);
                    Session["PasgoID"] = userDetails.PasgoID;
                    Session["FullName"] = userDetails.FullName;
                    Session["PhoneNumber"] = userDetails.PhoneNumber;
                    Session["Email"] = userDetails.Email;
                    Session["DOB"] = userDetails.DOB;
                    Session["Gender"] = userDetails.Gender_;
                    
                    if (userDetails.Level != 1)
                    {
                        Session["Level"] = userDetails.Level;
                        return RedirectToAction("Authentication", "Admin", userDetails);
                    }
                    if (userDetails.Locked != null && userDetails.Locked > DateTime.Now)
                    {
                        TempData["Failed"] = "Tài khoản bị khóa, vui lòng liên hệ CSKH!";
                        return RedirectToAction("Login");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
        }
       
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Session["PasgoID"] == null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [Obsolete]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.PasgoUser userModel)
        {
            using(PasGoEntities2 db = new PasGoEntities2())
            {
                //Nên để thành 1 private riêng cho chức năng InsertUser
                //Chuyển thành switch 
                if (ModelState.IsValid)
                {
                    userModel.Salt = CreatSalt();
                    userModel.PasswordHashed = Hashed(userModel.Password, userModel.Salt);
                    //Not part of the model for the current context
                    var result = Convert.ToInt32( db.InsertUser(userModel.FullName, userModel.PhoneNumber, userModel.Email, true, userModel.Salt, userModel.PasswordHashed).ToList().ElementAt(0));
                    if (result == 0)
                    {
                        TempData["Failed"] = "Đã có người sử dụng số điện thoại này, vui lòng nhập số điện thoại khác.";
                        return View("Register", userModel);
                    }
                    if (result == 1)
                    {
                        TempData["Failed"] = "Đã có người sử dụng Email này, vui lòng nhập Email khác.";
                        return View("Register", userModel);
                    }   
                    TempData["Success"] = "Đăng ký thành công";   
                }
                else
                {
                    TempData["Failed"] = "Đăng ký thất bại";
                    return View("Register", userModel);
                }
            }
            ModelState.Clear();
            return View("Register", new PasgoUser());
        }

        public ActionResult UserLandingView()
        {
            return View();
        }
    }
}