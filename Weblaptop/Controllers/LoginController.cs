using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblaptop.Models;

namespace Weblaptop.Controllers
{
    public class LoginController : Controller
    {
        dbQLBanHangDataContext db = new dbQLBanHangDataContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection frm)
        {
            var tendn = frm["TaiKhoan"];
            var matkhau = frm["MatKhau"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                }
                else
                    ViewBag.Thongbao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return this.Index();
        }
    }
}