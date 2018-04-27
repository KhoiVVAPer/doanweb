using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHD.Models;

namespace QLHD.Controllers
{
    public class AdminKhoaHocController : Controller
    {
        // GET: AdminKhoaHoc
        dbQLHDDataContext db = new dbQLHDDataContext();
        public ActionResult Index()
        {
            
            return View(db.Courses.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Course crs, FormCollection colle)
        {
            var id = int.Parse(colle["idCourse"]);
            var yt = int.Parse(colle["YearStart"]);
            var ye = int.Parse(colle["YearEnd"]);
            try
            {

                crs.idCourse = id;
                crs.YearStart = yt;
                crs.YearEnd = ye;


                db.Courses.InsertOnSubmit(crs);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["Error"] = "Đã xảy ra lỗi, vui lòng kiểm tra lại";
                return this.Create();
            }
        }
        [HttpGet]
        public ActionResult Edit(int idCourse)
        {
            Course crs = db.Courses.SingleOrDefault(n => n.idCourse == idCourse);
            if (crs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(crs);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int idCourse, FormCollection colle)
        {
            var id = int.Parse(colle["idCourse"]);
            var yt = int.Parse(colle["YearStart"]);
            var ye = int.Parse(colle["YearEnd"]);

            Course crs = db.Courses.SingleOrDefault(n => n.idCourse == idCourse);
            try
            {
                crs.idCourse = id;
                crs.YearStart = yt;
                crs.YearEnd = ye;

                UpdateModel(crs);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["Error"] = "Đã xảy ra lỗi, vui lòng kiểm tra lại";
                return this.Edit(id);
            }
        }
        [HttpGet]
        public ActionResult Details(int idCourse)
        {
            Course crs = db.Courses.SingleOrDefault(n => n.idCourse == idCourse);
            if (crs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(crs);
        }
        [HttpGet]
        public ActionResult Delete(int idCourse)
        {
            Course crs = db.Courses.SingleOrDefault(n => n.idCourse == idCourse);
            if (crs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(crs);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleted(int idCourse)
        {
            Course crs = db.Courses.SingleOrDefault(n => n.idCourse == idCourse);
            if (crs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                db.Courses.DeleteOnSubmit(crs);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Loi = "Không thể xóa, có dữ liệu từ bảng con đang sử dụng dữ liệu này, vui lòng kiểm tra lại!";
                return this.Delete(idCourse);
            }

        }
    }
}