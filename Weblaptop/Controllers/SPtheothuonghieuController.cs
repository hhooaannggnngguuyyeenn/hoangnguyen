using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weblaptop.Models;


namespace Weblaptop.Controllers
{
    public class SPtheothuonghieuController : Controller
    {
        Model1 db = new Model1();

        // GET: SPtheothuonghieu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TH()
        {
            var thuonghieu = from s in db.ThuongHieux select s;
            return PartialView(thuonghieu);
        }


    }
}