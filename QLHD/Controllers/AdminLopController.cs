using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHD.Models;

namespace QLHD.Controllers
{
    public class AdminLopController : Controller
    {
        // GET: AdminLop
        dbQLHDDataContext db = new dbQLHDDataContext();
        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse");
            ViewBag.idfalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idfalcuty", "name");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Class cl, FormCollection colle)
        {
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse");
            ViewBag.idfalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idfalcuty", "name");
            var name = colle["name"];
            var khoa = int.Parse(colle["idfalcuty"]);
            var khoahoc = int.Parse(colle["idCourse"]);

            try
            {
                
                cl.name = name;
                cl.idfalcuty = khoa;
                cl.idCourse = khoahoc;
               

                db.Classes.InsertOnSubmit(cl);
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
        public ActionResult Edit(int idClass)
        {
            Class cl = db.Classes.SingleOrDefault(n => n.idClass == idClass);
            if (cl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse",cl.idCourse);
            ViewBag.idfalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idfalcuty", "name",cl.idfalcuty);
            return View(cl);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int idClass, FormCollection colle)
        {
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse");
            ViewBag.idfalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idfalcuty", "name");

            var malop = int.Parse(colle["idClass"]);
            var ten = colle["name"];
            var khoa = int.Parse(colle["idfalcuty"]);
            var khoahoc = int.Parse(colle["idCourse"]);

            Class cl = db.Classes.SingleOrDefault(n => n.idClass == idClass);
            try
            {
                cl.name = ten;
                cl.idfalcuty = khoa;
                cl.idCourse = khoahoc;

                UpdateModel(cl);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["Error"] = "Đã xảy ra lỗi, vui lòng kiểm tra lại";
                return this.Edit(malop);
            }
        }
        [HttpGet]
        public ActionResult Details(int idClass)
        {
            Class cl = db.Classes.SingleOrDefault(n => n.idClass == idClass);
            if (cl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cl);
        }
        [HttpGet]
        public ActionResult Delete(int idClass)
        {
            Class cl = db.Classes.SingleOrDefault(n => n.idClass == idClass);
            if (cl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cl);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleted(int idClass)
        {
            Class cl = db.Classes.SingleOrDefault(n => n.idClass == idClass);
            if (cl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                db.Classes.DeleteOnSubmit(cl);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Loi = "Không thể xóa, có dữ liệu từ bảng con đang sử dụng dữ liệu này, vui lòng kiểm tra lại!";
                return this.Delete(idClass);
            }
            
        }
    }
}