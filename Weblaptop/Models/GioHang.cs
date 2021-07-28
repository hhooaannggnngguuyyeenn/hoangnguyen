using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Weblaptop.Models
{
    public class GioHang
    {
        Model1 data = new Model1 ();
        public int iMaTB { set; get; }
        public string sTenTB { set; get; }
        public string sAnh { set; get; }
        public double dDonGia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhTien
        {
            get { return iSoluong * dDonGia; }
        }
        public GioHang(int MaTB)
        {
            iMaTB = MaTB;
            ThietBi tb = data.ThietBis.Single(n => n.MaTB == iMaTB);
            sTenTB = tb.TenTB;
            sAnh = tb.Anh;
            dDonGia = double.Parse(tb.GiaBan.ToString());
            iSoluong = 1;
        }

        public GioHang()
        {
        }

        public static List<GioHang> lstGH = new List<GioHang>();
        
    }
}