using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblaptop.Models;

namespace Weblaptop.Controllers
{
    public class UserController : Controller
    {
        dbQLBanHangDataContext db = new dbQLBanHangDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection frm, KhachHang kh)
        {
            var hoten = frm["HoTen"];
            var tendn = frm["TaiKhoan"];
            var matkhau = frm["MatKhau"];
            var diachi = frm["DiaChi"];
            var email = frm["Mail"];
            var Sdt = frm["Sdt"];
            //var ngaysinh = String.Format("{0:MM/dd/yyyy}", frm["NgaySinh"]);
            var ngaysinh = frm["NgaySinh"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loirong"] = "Không Được Để Trống Họ Tên";
            }
            if (String.IsNullOrEmpty(tendn))
             {
                ViewData["Loirong1"] = "Không Được Để Trống Tên Đăng Nhập";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loirong2"] = "Không Được Để Trống Mật Khẩu";
            }
           if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loirong3"] = "Không Được Để Trống Địa Chỉ";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loirong4"] = "Không Được Để Trống Email";
            }
            if (String.IsNullOrEmpty(Sdt))
            {
                ViewData["Loirong5"] = "Không Được Để Trống SĐT";
            }
            if (String.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loirong6"] = "Thông Tin Không Được Để Trống, Vui lòng kiểm tra ! ";
            }
            else
            {
                kh.HoTen = hoten;
                kh.TaiKhoan = tendn;
                kh.MatKhau = matkhau;
                kh.DiaChiKH = diachi;
                kh.Email = email;
                kh.DienThoaiKH = Sdt;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
            }
            return this.DangKy();
        }



        //Đăng Nhập

    }
}