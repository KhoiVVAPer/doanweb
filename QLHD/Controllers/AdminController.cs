using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHD.Models;

namespace QLHD.Controllers
{
    public class AdminController : Controller
    {
        dbQLHDDataContext db = new dbQLHDDataContext();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DSHD()
        {
            return View(db.Event.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoiHD()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
    }
}