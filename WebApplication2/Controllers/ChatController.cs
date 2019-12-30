using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ChatController : Controller
    {
        private PasGoEntities db = new PasGoEntities();
        
        private bool CheckStaffAuthorize()
        {
            string phonenumber;
            if (Session["PhoneNumber"] == null)
                return false;
            phonenumber = Session["PhoneNumber"].ToString();
            if (db.PasgoUsers.Where(x => x.PhoneNumber == phonenumber).FirstOrDefault().Level != 1)
                return true;
            return false;
        }
        public ActionResult Index()
        {
            if (CheckStaffAuthorize() == true)
            {
                var idstaff = Convert.ToInt32( Session["PasgoID"]);
                List<Conversation> result = db.Conversations.Where(x => x.IdStaff == idstaff).ToList();

                return View(result);
            }else
                return RedirectToAction("Index", "Home", 1);
        }

        public ActionResult Customer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnlockConversation()
        {
            string x = Request.Form["unlockid"];
            var idconversation = Convert.ToInt32(Request.Form["unlockid"]);
            var result = db.UpdateConversationStatus(idconversation, true);
            return RedirectToAction("Index", "Chat");
        }
        [HttpPost]
        public ActionResult LockConversation()
        {
            string x = Request.Form["lockid"];
            var idconversation = Convert.ToInt32(Request.Form["lockid"]);
            var result = db.UpdateConversationStatus(idconversation, false);
            return RedirectToAction("Index", "Chat");
        }
    }
}