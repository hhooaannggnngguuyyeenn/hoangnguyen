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
        public ActionResult Login(FormCollection frm)
        {
            var tendn = frm["TaiKhoan"];
            var matkhau = frm["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhập tài khoản";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh.HoTen.ToString();
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                    ViewBag.Thongbao = "Tài khoản hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index","TrangChu");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
    }
}