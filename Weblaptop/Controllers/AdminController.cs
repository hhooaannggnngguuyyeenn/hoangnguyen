using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblaptop.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Weblaptop.Controllers
{
    public class AdminController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        public ActionResult adTH()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.ThuongHieux.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["tkadmin"];
            var matkhau = collection["mkadmin"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");

                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult ThemmoiTH()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemmoiTH(ThuongHieu th)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            db.ThuongHieux.Add(th);
            db.SaveChanges();
            return RedirectToAction("adTH");
        }

        [HttpGet]
        public ActionResult SuaTH(int? id)
        {

            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            ThuongHieu adth = db.ThuongHieux.SingleOrDefault(n => n.MaTH == id);

            if (adth == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(adth);

        }
        [HttpPost, ActionName("SuaTH")]
        public ActionResult AcceptSuaTH(int? id)
        {

            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThuongHieu adth = db.ThuongHieux.SingleOrDefault(n => n.MaTH == id);
            UpdateModel(adth);
            db.SaveChanges();
            return RedirectToAction("adTH");
        }
        [HttpGet]
        public ActionResult XoaTH(int? id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            ThuongHieu adth = db.ThuongHieux.SingleOrDefault(n => n.MaTH == id);

            if (adth == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(adth);
        }

        [HttpPost, ActionName("XoaTH")]
        public ActionResult AcceptXoaTH(int? id)
        {

            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThuongHieu adth = db.ThuongHieux.SingleOrDefault(n => n.MaTH == id);
            db.ThuongHieux.Remove(adth);
            db.SaveChanges();
            return RedirectToAction("adTH");
        }
        //Thiết Bị
        public ActionResult TB(int? page)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(db.ThietBis.ToList().OrderBy(n => n.MaTB).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemmoiTB()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.MaTH = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            return View();
        }
        [HttpPost]
        public ActionResult ThemmoiTB(ThietBi tb, HttpPostedFileBase fileUpload, FormCollection frm)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.MaTH = new SelectList(db.ThuongHieux.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Dzui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                if (System.IO.File.Exists(path))
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                else
                {
                    fileUpload.SaveAs(path);
                }
                tb.Anh = fileName;
                tb.Mota = frm["txtMota"];

                db.ThietBis.Add(tb);
                db.SaveChanges();
            }
            return RedirectToAction("tb");
        }

        public ActionResult ChitietTB(int? id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThietBi tb = db.ThietBis.SingleOrDefault(n => n.MaTB == id);
            ViewBag.MaTB = tb.MaTB;
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tb);

        }

        [HttpGet]
        public ActionResult XoaTB(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThietBi tb = db.ThietBis.SingleOrDefault(n => n.MaTB == id);
            ViewBag.MaTB = tb.MaTB;
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tb);
        }
        [HttpPost, ActionName("XoaTB")]
        public ActionResult XacnhanxoaTB(int? id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThietBi tb = db.ThietBis.SingleOrDefault(n => n.MaTB == id);
            ViewBag.MaTB = tb.MaTB;
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ThietBis.Remove(tb);
            db.SaveChanges();
            return RedirectToAction("TB");
        }
        [HttpGet]
        public ActionResult SuaTB(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThietBi tb = db.ThietBis.SingleOrDefault(n => n.MaTB == id);
            ViewBag.MaTB = tb.MaTB;
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(tb);
        }
        [HttpPost]
        public ActionResult SuaTB(int id, FormCollection frm, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            ThietBi tb = db.ThietBis.SingleOrDefault(n => n.MaTB == id);
            ViewBag.MaTB = tb.MaTB;
            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Content/images/"), fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        ViewBag.ThongBao = "Hình đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(filePath);
                        tb.Anh = fileName;
                    }
                }
                tb.NgayCapNhat = DateTime.Parse(frm["NgayCapNhat"]);
                tb.Mota = frm["Mota"].ToString();
                UpdateModel(tb);
                db.SaveChanges();
            }
            return RedirectToAction("TB");
        }
    }
}
