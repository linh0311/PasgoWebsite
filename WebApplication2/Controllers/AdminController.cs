using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private PasGoEntities2 db = new PasGoEntities2();
        // GET: Admin
        public ActionResult Index()
        {
            if(0 == 0)
                return View();
        }

        public ActionResult Authentication(Models.Login_Result user)
        {
            if(user.Level == 99)
                return RedirectToAction("Index", "Admin");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Admin()
        {

            return View();
        }
        public ActionResult NhaHang()
        {
            
            return View();
        }
        public ActionResult NguoiDung()
        {
            return View();
        }
    }
}