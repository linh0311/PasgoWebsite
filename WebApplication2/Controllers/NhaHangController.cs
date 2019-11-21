using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class NhaHangController : Controller
    {
        private PasGoEntities2 db = new PasGoEntities2();
        // GET: NhaHang

        public ActionResult Index()
        {
            //Loai chưa có giá trị cụ thể trong database - làm sau
            //var loai = Request.Form["Loai"];
            var thanhpho = Convert.ToInt32(Request.Form["ThanhPho"]);
            var khuyenmai = Convert.ToInt32(Request.Form["KhuyenMai"]);
            var soluong = Convert.ToInt32(Request.Form["SoLuong"]);
            var timkiem = Request.Form["searchbox"];
            //Nếu giá trị null thì sẽ đưua vào SQL sau.
            var result = db.SimpleRestaurant(thanhpho, khuyenmai, soluong, timkiem);
            //Lưu vào để còn so sánh lấy giá trị selected - làm sau
            return View("Index", result.ToList());
        }

        public ActionResult Details(int? Rid)
        {
            if (Rid == null)
                return RedirectToAction("Index", "NhaHang");
            var a = db.FullInfoRestaurant(Rid).Count();
            if (a == 0)
            {
                TempData["Failed"] = "Không tồn tại nhà hàng trong CSDL.";
                return RedirectToAction("Index", "NhaHang");
            }
            PasgoRestaurant results = db.FullInfoRestaurant(Rid).ElementAt(0);
           
            List<allImg_Result> resultsimg = new List<allImg_Result>();
            List<thongtinthemSP_Result> resulttt = new List<thongtinthemSP_Result>();
            //Nếu nhà hàng không có trong danh sách thuộc thương hiệu thì ds trả về = null -> lỗi, không push object null đc
            var listchuoi = db.ThuocThuongHieuChuoi(Rid);
            ThuocThuongHieuChuoi_Result resultchuoi = new ThuocThuongHieuChuoi_Result();
            ThuocThuongHieuChuoi_Result thaydoi = new ThuocThuongHieuChuoi_Result { IdThuongHieu = 0, Name = "(Chưa cập nhật)", AddressDT = "(Chưa cập nhật)", LinkTH = "https://via.placeholder.com/400x210.png?text=ch%C6%B0a+c%E1%BA%ADp+nh%E1%BA%ADt", ImgDT = "https://via.placeholder.com/400x210.png?text=ch%C6%B0a+c%E1%BA%ADp+nh%E1%BA%ADt", ImgTH = "(Chưa cập nhật)" };
            if (listchuoi.Count() == 0)
                resultchuoi = thaydoi;
            else
                resultchuoi = db.ThuocThuongHieuChuoi(Rid).ElementAt(0);
            var rsimg = db.allImg(Rid);
            var rstt = db.thongtinthemSP(Rid);
            foreach (var item in rsimg.ToList())
            {
                resultsimg.Add(item);
            }
            foreach (var item in rstt.ToList())
            {
                resulttt.Add(item);
            }
            return View(Tuple.Create(results, resultsimg.ToList(), resulttt.ToList(), resultchuoi));
        }

        public ActionResult Thongtin()
        {
            List<thongtinthemSP_Result> resulttt = new List<thongtinthemSP_Result>();
            var rstt = db.thongtinthemSP(1);
            foreach (var item in rstt.ToList())
            {
                resulttt.Add(item);
            }
            return View(resulttt.ToArray());
        }

        public ActionResult Thuonghieu()
        {
            ThuocThuongHieuChuoi_Result result = db.ThuocThuongHieuChuoi(15).ElementAt(0);
            return View(result);
        }

        public ActionResult UpdateDetails(int? Rid)
        {
            if (Rid == null)
                return RedirectToAction("Index", "NhaHang");
            PasgoRestaurant results = db.FullInfoRestaurant(Rid).ElementAt(0);
            return View(results);
        }
        [HttpPost]
        public ActionResult UpdateDetails(PasgoRestaurant R)
        {
            System.Diagnostics.Trace.WriteLine("test post");
            System.Diagnostics.Trace.WriteLine(R.PasgoRID.ToString());
            System.Diagnostics.Trace.WriteLine(R.RName);
            var ok = db.UpdateInsertRestaurantInfo(R.PasgoRID, R.RName, R.Rlink, R.Rpromotion, R.Rstar, R.Rdollar,
                R.Raddress, R.Rcity, R.RpromotionPercent, R.Rfeature, R.RLoaiHinh, R.RPhuHop, R.RKhongGian,
                R.RChoDeXe, R.RDacTrung, R.RThongTinThem, R.RGioiThieu, 1).ToList().ElementAt(0);
            //Update thành công nhưng không nhận được thông số?
            //ok này trả về một list chứa 1 element và có duy nhất 1 cột thông báo        
            int status = Convert.ToInt32(ok);
            if (status == 0)
                ViewBag.UpdateFailed = "Xảy ra lỗi, cập nhật thất bại.";
            if (status == 1)
                ViewBag.UpdateSuccess = "Cập nhật thông tin thành công.";
            if (status == 2)
                ViewBag.UpdateSuccess = "Không tìm thấy nhà hàng, cập nhật thất bại.";
            return View(R);
        }

        public ContentResult Picture(string x)
        {
            string html = "<img alt=" + x + " class='img-indicators2' src=" + x + ">";
            return Content(html, "text/html", System.Text.Encoding.UTF8);
        }

        //Không có status
        public ActionResult Updatethongtin(int? Rid)
        {
            if (Rid == null)
                return RedirectToAction("Index", "NhaHang");
            List<thongtinthemSP_Result> result = new List<thongtinthemSP_Result>();
            var rs = db.thongtinthemSP(Rid);
            foreach (var item in rs.ToList())
            {
                result.Add(item);
            }
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult Updatethongtin(int id, Boolean[] out1, Boolean[] out2, Boolean[] out3, Boolean[] out4, Boolean[] out5, Boolean[] out6,
                            Boolean[] out7, Boolean[] out8, Boolean[] out9, Boolean[] out10, Boolean[] out11, Boolean[] out12, Boolean[] out13, Boolean[] out14)
        {
            //Boolean[] out0, Boolean[] out1, Boolean[] out2, Boolean[] out3, Boolean[] out4, Boolean[] out5, Boolean[] out6,
            //Boolean[] out7, Boolean[] out8, Boolean[] out9, Boolean[] out10, Boolean[] out11, Boolean[] out12, Boolean[] out13
            System.Diagnostics.Trace.WriteLine("test post------------------------------------");
            var ok = db.UpdateThongtinthem(id, out1[0], out2[0], out3[0], out4[0], out5[0], out6[0], out7[0], out8[0], out9[0], out10[0],
                                            out11[0], out12[0], out13[0], out14[0]).ToList().ElementAt(0);
            var status = Convert.ToInt32(ok);
            System.Diagnostics.Trace.WriteLine(status.ToString());
            if (status == 0)
                TempData["Failed"] = "Xảy ra lỗi, cập nhật thất bại.";
            if (status == 1)
                TempData["Success"] = "Cập nhật thông tin thành công.";
            return RedirectToAction("updatethongtin", "NhaHang", new { Rid = id });
        }
        //Update/Delete Chuỗi nhà hàng phụ thuộc.
        public ActionResult UpdateChuoi(int? nhahang)
        {
            if (nhahang == null)
                return RedirectToAction("Index", "NhaHang");
            var listresult = db.ThuocThuongHieuChuoi(nhahang);
            ThuocThuongHieuChuoi_Result resultchuoi = new ThuocThuongHieuChuoi_Result();
            List<ThuocThuongHieuChuoi_Result> resulttim = new List<ThuocThuongHieuChuoi_Result>();
            ThuocThuongHieuChuoi_Result thaydoi = new ThuocThuongHieuChuoi_Result { IdThuongHieu = 0, Name = "(Chưa cập nhật)", AddressDT = "(Chưa cập nhật)", LinkTH = "https://via.placeholder.com/400x210.png?text=ch%C6%B0a+c%E1%BA%ADp+nh%E1%BA%ADt", ImgDT = "https://via.placeholder.com/400x210.png?text=ch%C6%B0a+c%E1%BA%ADp+nh%E1%BA%ADt", ImgTH = "(Chưa cập nhật)" };
            if (listresult.Count() == 0)
                resultchuoi = thaydoi;
            else
                resultchuoi = db.ThuocThuongHieuChuoi(nhahang).ElementAt(0);
            var search = db.SearchChuoi("");
            foreach (var item in search.ToList())
            {
                resulttim.Add(item);
            }
            return View(Tuple.Create(resultchuoi, resulttim.ToList(), Convert.ToInt32(nhahang), thaydoi));
        }

        [HttpPost]
        public ActionResult UpdateChuoi(int nhahang, string searchbox, ThuocThuongHieuChuoi_Result thaydoi)
        {
            ThuocThuongHieuChuoi_Result resultchuoi = db.ThuocThuongHieuChuoi(nhahang).ElementAt(0);
            List<ThuocThuongHieuChuoi_Result> resulttim = new List<ThuocThuongHieuChuoi_Result>();
            var search = db.SearchChuoi(searchbox);
            foreach (var item in search.ToList())
            {
                resulttim.Add(item);
            }
            return View(Tuple.Create(resultchuoi, resulttim.ToList(), nhahang, thaydoi));
        }

        [HttpPost]
        public ActionResult UpdateChuoiID(int nhahang, int chuoibandau, int chuoimoi)
        {
            var search = db.UpdateChuoi(nhahang, chuoibandau, chuoimoi, 1);
            System.Diagnostics.Trace.WriteLine("chuỗi mới");
            System.Diagnostics.Trace.WriteLine(chuoimoi.ToString());
            if (search == 0)
                TempData["Failed"] = "Xảy ra lỗi, cập nhật thất bại.";
            if (search == 1)
                TempData["Success"] = "Cập nhật thông tin thành công.";
            return RedirectToAction("UpdateChuoi", new { nhahang = nhahang });
        }

        public ActionResult DeleteChuoiID(int? nhahang)
        {
            if (nhahang == null)
                return RedirectToAction("Index", "NhaHang");
            var search = db.UpdateChuoi(nhahang, 1, 1, 0);
            if (search == 0)
                TempData["Failed"] = "Xảy ra lỗi, cập nhật thất bại.";
            if (search == 1)
                TempData["Success"] = "Cập nhật thông tin thành công.";
            return RedirectToAction("UpdateChuoi", new { nhahang = nhahang });
        }

        public ActionResult UpdatePic(int? Rid, int? vitri)
        {
            if (Rid == null || vitri == null)
                return RedirectToAction("Index");
            List<allImg_Result> result = new List<allImg_Result>();
            var rs = db.allImg(Rid);
            foreach (var item in rs.ToList())
            {
                result.Add(item);
            }
            return View(Tuple.Create(result.ToList(), Convert.ToInt32(Rid), Convert.ToInt32(vitri)));
        }

        public ActionResult Update1Pic()
        {
            var id = Request.Form["showid"];
            var vitri = Request.Form["showthutu"];
            var nhahang = Request.Form["nhahangid"];
            var link = Request.Form["finalduongdan"];
            var mota = Request.Form["showmota"];
            var loai = Request.Form["loai"];
            var result = db.UpdateIMG(Convert.ToInt32(id), link, mota, Convert.ToInt32(loai), Convert.ToInt32(nhahang), 1);
            //Lỗi ảnh trống. check link ảnh newduongdan trước khi cho vào.
            // Mô tả sẽ kích hoạt btn cập nhật, nhưng không kiểm tra 
            if (result == 0)
                TempData["Failed"] = "Xảy ra lỗi, cập nhật thất bại.";
            if (result == 1)
                TempData["Success"] = "Cập nhật thông tin thành công.";
            //Đưa nhầm Rid là id của ảnh -> sau update ảnh tự chạy sang nhà hàng khác
            //Đã sửa
            return RedirectToAction("UpdatePic", "NhaHang", new {Rid = nhahang, vitri = vitri});
        }

        public ActionResult InsertPic(int? id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult Insert1Pic()
        {
            var src = Request.Form["newduongdan"];
            var mota = Request.Form["showmota"];
            var loai = Convert.ToInt32(Request.Form["loai"]);
            var id = Convert.ToInt32(Request.Form["rid"]);
            var result = db.UpdateIMG(0, src, mota, loai, id, 2);
            System.Diagnostics.Trace.WriteLine("insert: ");
            System.Diagnostics.Trace.WriteLine(src +" / "+ mota + " / " + loai.ToString() + " / result: " + result) ;
            if (result == 0)
                TempData["Failed"] = "Xảy ra lỗi, cập nhật thất bại.";
            if (result == 1)
                TempData["Success"] = "Cập nhật thông tin thành công.";
            return RedirectToAction("InsertPic", "NhaHang", new { id = id });
        }

        public ActionResult DeletePic()
        {
            var confirm = Request.Form["confirmdelete"].ToString();
            var id = Convert.ToInt32( Request.Form["deleteID"]);
            var nhahang = Request.Form["nhahangid"];
            System.Diagnostics.Trace.WriteLine("delete: ");
            System.Diagnostics.Trace.WriteLine(confirm + " / " + id);
            if (confirm == "true" || id == 0)
            {
                var result = db.UpdateIMG(id, "del", null, 0, Convert.ToInt32(nhahang), 0); 
                if (result == 0)
                TempData["Failed"] = "Xảy ra lỗi, xóa ảnh thất bại.";
                if (result == 1)
                    TempData["Success"] = "Xoá ảnh thành công.";
                System.Diagnostics.Trace.WriteLine("status: "+result.ToString());
            }
            else
                TempData["Failed"] = "Thao tác xóa bị hủy";
            return RedirectToAction("UpdatePic", "NhaHang", new { Rid = nhahang, vitri = 1 });
        }
        
        public ActionResult CreatNhaHang()
        {
            ViewBag.RID = "none";
            ViewBag.Promotions = db.Promotions.ToList();
            ViewBag.Cities = db.Cities.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreatNhaHang(PasgoRestaurant model)
        {
            ViewBag.Promotions = db.Promotions.ToList();
            ViewBag.Cities = db.Cities.ToList();
            if (model.RpromotionPercent != 0)
                model.Rpromotion = model.Rpromotion + " " + model.RpromotionPercent + "%";
            var result = db.UpdateInsertRestaurantInfo (0, model.RName, model.Rlink, model.Rpromotion, model.Rstar, model.Rdollar, model.Raddress, model.Rcity,
                model.RpromotionPercent, model.Rfeature, model.RLoaiHinh, model.RPhuHop, model.RKhongGian, model.RChoDeXe, model.RDacTrung, model.RThongTinThem,
                model.RGioiThieu, 2).ToList().ElementAt(0);
            System.Diagnostics.Trace.WriteLine("add: ");
            System.Diagnostics.Trace.WriteLine(model.Raddress + " / " + model.RChoDeXe + " / " + model.Rcity + " / " + model.Rdollar + " / " + model.Rstar);
            int status = Convert.ToInt32(result);
            ViewBag.RID = "none";
            if (status == 0)
                ViewBag.UpdateFailed = "Tạo nhà hàng thất bại";
            if (status == 2)
                ViewBag.UpdateFailed = "Tạo nhà hàng thất bại. Nhà hàng tạo có tên/địa chỉ/đường dẫn bị trùng với nhà hàng khác trong CSDL.";
            if (status != 0 && status != 2)
            {
                ViewBag.UpdateSuccess = "Tạo nhà hàng thành công.";
                ViewBag.RID = status;
            }
            return View(model);
        }

        public ActionResult DeleteNhaHang()
        {
            var confirm = Request.Form["confirmdelete"].ToString();
            System.Diagnostics.Trace.WriteLine("delete? "+confirm);
            var nhahang = Convert.ToInt32( Request.Form["nhahangid"]);
            if(confirm != "true" || nhahang == 0)
            {
                TempData["Failed"] = "Hành động xóa đã bị hủy.";
                return RedirectToAction("Details", "NhaHang", new { Rid = nhahang });
            }
            var result = db.UpdateInsertRestaurantInfo(nhahang, "null", "null", "null", 1, 1, "null", 1, 1, "null", null, null, null, null, null, "null", "null", 0).ToList().ElementAt(0);
            int x = Convert.ToInt32(result);
            System.Diagnostics.Trace.WriteLine("ketqua: " + x.ToString());
            if (x == 0)
                TempData["Failed"] = "Xảy ra lỗi, xóa nhà hàng thất bại.";
            if (x != 0 && x != 999)
                TempData["Success"] = "Xoá nhà hàng thành công.";
            if (x == 999)
                TempData["Failed"] = "Nhà hàng không tồn tại trong CSDL.";
            return RedirectToAction("Index", "NhaHang");
        }
    }
}