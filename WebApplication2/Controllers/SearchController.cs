﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication2.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.IO;
using RazorEngine;

namespace WebApplication2.Controllers
{

    public class SearchController : ApiController
    {
        private PasGoEntities db = new PasGoEntities();

        //Test chưa hoàn thành, đã có cách khác tốt hơn
        public string RenderViewToString (ControllerBase controller, string viewPath, object model)
        {
            controller.ViewData.Model = model;
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewPath);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

       
        //Test Tìm kiếm tất cả nhà hàng : Hoạt động
        public IEnumerable<SimpleRestaurant_Result> Get()
        {
            List<SimpleRestaurant_Result> result = db.SimpleRestaurant(null, null, null, null).ToList();
            return result;
        }

        //Test Tìm kiếm nhà hàng với tên = keyword : Hoạt động
        public IEnumerable<SimpleRestaurant_Result> Get(string name)
        {
            
            List<SimpleRestaurant_Result> result = db.SimpleRestaurant(null, null, null, name).ToList();
            return result;
        }
        //Đã lỗi thời - cần thay mới
        //Đây mới chỉ là phương thức GET / phải thay đổi sang POST
        //API link trả về có parameter - Remove querystring from URL
        [System.Web.Http.HttpGet]
        [Obsolete]
        public HttpResponseMessage Get(string city, string keyword)
        {
            if (keyword == null)
                keyword = "";
            var result = db.APIsearch(Convert.ToInt32(city), 1, keyword).ToList();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string viewPath = HttpContext.Current.Server.MapPath(@"~/Views/API/APIresultNhaHang1.cshtml");
            var template = File.ReadAllText(viewPath);
            string parsedView = Razor.Parse(template, result);
            //response kèm luôn encode và type, nếu set riêng dễ lỗi
            response.Content = new StringContent(parsedView, System.Text.Encoding.UTF8, "text/html"); 

            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html;charset=utf-8");
            //response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()

            return response;
        }

   
        //Call API từ js hoặc jQ


    }
}
