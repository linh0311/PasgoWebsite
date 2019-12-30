using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Security;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        
        private PasGoEntities db = new PasGoEntities();
        // GET: Admin


        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["Level"]) == 1 || Session["Level"] == null)
                return RedirectToAction("Index", "Home");
            if (0 == 0)
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