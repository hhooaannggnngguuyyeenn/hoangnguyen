using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblaptop.Models;


namespace Weblaptop.Controllers
{
    public class GioHangController : Controller
    {
        

        // GET: GioHang
        public ActionResult getcart()
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if (lstGH == null)
            {
                return RedirectToAction("index", "trangchu");

            }
            Session["tongtien"] = TongTien();
            return View( lstGH);
        }
        Model1 db = new Model1();
        public ActionResult addcart(int MaTB)
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            ThietBi tba = db.ThietBis.FirstOrDefault(p => p.MaTB == MaTB);
            GioHang a = new GioHang();
            if (lstGH == null)
            {
                lstGH = new List<GioHang>();
                a.iMaTB = tba.MaTB;
                a.iSoluong = 1;
                a.sAnh =  tba.Anh;
                a.sTenTB = tba.TenTB;
                a.dDonGia = double.Parse(tba.GiaBan.ToString());
                lstGH.Add(a);              
            }
            else
            {
              var b =  lstGH.FirstOrDefault(p => p.iMaTB == MaTB);
                if (b==null)
                {
                    a.iMaTB = tba.MaTB;
                    a.iSoluong = 1;
                    a.sAnh = tba.Anh;
                    a.sTenTB = tba.TenTB;
                    a.dDonGia = double.Parse(tba.GiaBan.ToString());
                    lstGH.Add(a);
                }
                else
                {
                    b.iSoluong += 1;
                }
              
            }
           Session["tongtien"] = TongTien();
            Session["GioHang"] = lstGH;
            return RedirectToAction("getcart");
        }
        
        public ActionResult removecart(int MaTB)
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            GioHang gh1 = lstGH.FirstOrDefault(p => p.iMaTB == MaTB);
           // ThietBi tba = db.ThietBi.FirstOrDefault(p => p.MaTB == MaTB);
            if (lstGH != null)
            {
                lstGH.Remove(gh1);
                if (lstGH.Count == 0)
                {
                    return RedirectToAction("Index", "TrangChu");
                }
                return RedirectToAction("getcart");
            }

            Session["tongtien"] = TongTien();
            return RedirectToAction("getcart");
        }

        public ActionResult editcart(int? MaTB, FormCollection TB)
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            //var MaTB = TB["iMaTB"];
            var sl = int.Parse(TB["SL"]);
            if (lstGH != null)
            {
                lstGH.FirstOrDefault(p => p.iMaTB.Equals(MaTB)).iSoluong = sl;
            }
            Session["GioHang"] = lstGH;
            Session["tongtien"] = TongTien();
            return View("getcart", lstGH);
        }

        public ActionResult DatHang()
        {
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = Session["KH"] as KhachHang;
            List<GioHang> gh = Session["GioHang"] as List<GioHang>;
            if (kh == null)
            {
                return RedirectToAction("Login","Login");
            }
            else
            {
                ddh.NgayDat = DateTime.Now;
                var ngaygiao = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
                ddh.NgayGiao = DateTime.Parse(ngaygiao);
                ddh.TinhTrangGiaoHang = false;
                ddh.DaThanhToan = false;
                ddh.Tongtien = TongTien();
                db.DonDatHangs.Add(ddh);
                db.SaveChanges();
                foreach (var item in gh)
                {
                    ChiTietDonDatHang ctddh = new ChiTietDonDatHang();
                    ctddh.MaDH = ddh.MaDH;
                    ctddh.MaTB = item.iMaTB;
                    ctddh.SoLuong = item.iSoluong;
                    ctddh.DonGia = (float)item.dDonGia;
                    ctddh.TongTien = TongTien();
                    db.ChiTietDonDatHangs.Add(ctddh);
                }
                db.SaveChanges();
                Session["GioHang"] = null;
            }
            return RedirectToAction("getcart", "GioHang");
        }
        
        public ActionResult XacNhanDatHang()
        {
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if (lstGH == null)
            {
                return RedirectToAction("index", "trangchu");

            }
            Session["tongtien"] = TongTien();
            return View(lstGH);
        }
  
        private double TongTien()
            {
                double iTongTien = 0;
                List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
                if (lstGioHang != null)
                {
                    iTongTien = lstGioHang.Sum(n => n.dThanhTien);
                }
                return iTongTien;
            }
        public ActionResult deleteall()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            lstGioHang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }
            //public ActionResult GioHang()
            //{
            //    List<GioHang> lstGioHang = getcart();reu
            //    if (lstGioHang.Count == 0)
            //    {
            //        return RedirectToAction("Index", "GioHang");
            //    }
            //    ViewBag.TongSoLuong = TongSoLuong();
            //    ViewBag.TongTien = TongTien();
            //    return View(lstGioHang);
            //}
    }
}