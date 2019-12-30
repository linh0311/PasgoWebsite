using System;
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

    public class UploadController : ApiController
    {
        
        private PasGoEntities db = new PasGoEntities();

        //Test chưa hoàn thành, đã có cách khác tốt hơn
        public string RenderViewToString(ControllerBase controller, string viewPath, object model)
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
        //Đã lỗi thời - cần thay mới
        //Đây mới chỉ là phương thức GET / phải thay đổi sang POST
        //API link trả về có parameter - Remove querystring from URL
        [System.Web.Http.HttpPost]
        public HttpResponseMessage PostAvatar()
        {
            Guid g = Guid.NewGuid();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            int maxLength = 1024 * 1024 * 1;
            //dat bien vao day, cuoi try check ok hoac fail
            try
            {
                //StreamReader stream = new StreamReader(HttpContext.Current.Request.InputStream);
                //var data = stream.ReadToEnd();
                var httprequest = HttpContext.Current.Request;
                var pasgoid = Convert.ToInt32(httprequest.Form["PasgoID"]);
                //test gia tri di kem
                foreach (string key in httprequest.Form)
                {
                    System.Diagnostics.Debug.WriteLine("key - "+ key + "/value : " + httprequest.Form[key]);
                }

                foreach (string file in httprequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    var postedFile = httprequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength != 0)
                    {
                        IList<string> allowExtension = new List<string> { ".jpg", ".img", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("."));
                        var name = Path.GetFileNameWithoutExtension(postedFile.FileName);
                        System.Diagnostics.Trace.WriteLine("xxx" + name);
                        var extension = ext.ToLower();
                        if (!allowExtension.Contains(extension))
                        {
                            dict.Add("message", string.Format("Định dạng ảnh không phù hợp!"));
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > maxLength)
                        {
                            dict.Add("message", string.Format("Ảnh có kích thước quá lớn!"));
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            var mapPath = HttpContext.Current.Server.MapPath("~/Upload/Avatar/" + g.ToString() + extension);
                            postedFile.SaveAs(mapPath);
                        }
                        var cf = db.UpdateAvatar(pasgoid, g.ToString()+ extension);
                        if(Convert.ToInt32(cf.ToList().ElementAt(0)) != 0)
                            dict.Add("message", string.Format("Upload ảnh thành công!"));
                        else
                            dict.Add("message", string.Format("Upload ảnh thất bại!"));
                        return Request.CreateResponse(HttpStatusCode.Created, dict);
                    }
                }
                dict.Add("message", string.Format("Chọn một ảnh!"));
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception e)
            {
                dict.Add("message", string.Format("Booooo!"));
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }


        //Call API từ js hoặc jQ


    }
}
