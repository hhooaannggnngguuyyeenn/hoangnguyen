using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblaptop.Models;

namespace Weblaptop.Controllers
{
    public class TrangChuController : Controller
    {
        Model1 db = new Model1();
        // GET: TrangChu
        public ActionResult Index()
        {
                var tb = db.ThietBis.Take(4).ToList();
                return View(tb);
        }
        public ActionResult ChiTiet(int id)
        {

            var tb = db.ThietBis.FirstOrDefault(p => p.MaTB == id);
            //from s in db.ThietBi s
            //         where s.MaTB == id
            //         select s;

            return View(tb);
        }

        public ActionResult SP(int id)
        {
            var sp = from s in db.ThietBis where s.MaTH == id select s;
            return View(sp);
        }
    }
}