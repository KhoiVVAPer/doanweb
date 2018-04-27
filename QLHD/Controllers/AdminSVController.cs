using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHD.Models;
using QLHD.ViewModel;

namespace QLHD.Controllers
{
    public class AdminSVController : Controller
    {
        // GET: AdminSV
        dbQLHDDataContext db = new dbQLHDDataContext();
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<LoaiSV> listLoai = new List<LoaiSV>();
            LoaiSV loai1 = new LoaiSV();
            LoaiSV loai2 = new LoaiSV();
            loai1.val = true;
            loai1.name = "Có";
            listLoai.Add(loai1);
            loai2.val = false;
            loai2.name = "Không";
            listLoai.Add(loai2);
            ViewBag.idType = new SelectList(listLoai, "val", "name");
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name");
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse");
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Student sv, FormCollection colle)
        {

            List<LoaiSV> listLoai = new List<LoaiSV>();
            LoaiSV loai1 = new LoaiSV();
            LoaiSV loai2 = new LoaiSV();
            loai1.val = true;
            loai1.name = "Có";
            listLoai.Add(loai1);
            loai2.val = false;
            loai2.name = "Không";
            listLoai.Add(loai2);
            ViewBag.idType = new SelectList(listLoai, "val", "name");
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name");
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse");
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name");

            var masv = colle["idStudent"];
            var ten = colle["name"];
            var ns = String.Format("{0:MM/dd/yyyy}", colle["dob"]);
            var lop = int.Parse(colle["idClass"]);
            var khoa = int.Parse(colle["idFalcuty"]);
            var khoahoc = int.Parse(colle["idCourse"]);
            var bcs = bool.Parse(colle["idType"]);
            var diachi = colle["andress"];
            var email = colle["email"];
            var sdt = colle["phone"];
            try
            {
                sv.idStudent = masv;
                sv.name = ten;
                sv.dob = DateTime.Parse(ns);
                sv.idCourse = khoahoc;
                sv.idFalcuty = khoa;
                sv.idClass = lop;
                sv.idType = bcs;
                sv.andress = diachi;
                sv.email = email;
                sv.phone = sdt;

                db.Students.InsertOnSubmit(sv);
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
        public ActionResult Edit(string idStudent)
        {

            Student sv = db.Students.SingleOrDefault(n => n.idStudent.CompareTo(idStudent) == 0);
            if (sv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<LoaiSV> listLoai = new List<LoaiSV>();
            LoaiSV loai1 = new LoaiSV();
            LoaiSV loai2 = new LoaiSV();
            loai1.val = true;
            loai1.name = "Có";
            listLoai.Add(loai1);
            loai2.val = false;
            loai2.name = "Không";
            listLoai.Add(loai2);
            ViewBag.idType = new SelectList(listLoai, "val", "name", sv.idType);
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name", sv.idClass);
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse", sv.idCourse);
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name", sv.idFalcuty);
            return View(sv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string idStudent, FormCollection colle)
        {


            List<LoaiSV> listLoai = new List<LoaiSV>();
            LoaiSV loai1 = new LoaiSV();
            LoaiSV loai2 = new LoaiSV();
            loai1.val = true;
            loai1.name = "Có";
            listLoai.Add(loai1);
            loai2.val = false;
            loai2.name = "Không";
            listLoai.Add(loai2);
            ViewBag.idType = new SelectList(listLoai, "val", "name");
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name");
            ViewBag.idCourse = new SelectList(db.Courses.ToList().OrderBy(n => n.idCourse), "idCourse", "idCourse");
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name");


            var masv = colle["idStudent"];
            var ten = colle["name"];
            var dob = String.Format("{0:MM/dd/yyyy}", colle["dob"]);
            var lop = int.Parse(colle["idClass"]);
            var khoa = int.Parse(colle["idFalcuty"]);
            var khoahoc = int.Parse(colle["idCourse"]);
            var bcs = bool.Parse(colle["idType"]);
            var diachi = colle["andress"];
            var email = colle["email"];
            var sdt = colle["phone"];

            Student sv = db.Students.First(n => n.idStudent.CompareTo(masv) == 0);
            try
            {
                sv.name = ten;
                sv.dob = DateTime.Parse(dob);
                sv.idFalcuty = khoa;
                sv.idCourse = khoahoc;
                sv.idFalcuty = khoa;
                sv.idClass = lop;
                sv.idType = bcs;
                sv.andress = diachi;
                sv.email = email;
                sv.phone = sdt;

                UpdateModel(sv);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["Error"] = "Đã xảy ra lỗi, vui lòng kiểm tra lại";
                return this.Edit(masv);
            }
        }
        [HttpGet]
        public ActionResult Details(string idStudent)
        {
            Student sv = db.Students.SingleOrDefault(n => n.idStudent.CompareTo(idStudent) == 0);
            if (sv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sv);
        }
        [HttpGet]
        public ActionResult Delete(string idStudent)
        {
            Student sv = db.Students.SingleOrDefault(n => n.idStudent.CompareTo(idStudent) == 0);
            if (sv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sv);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Deleted(string idStudent)
        {
            Student sv = db.Students.SingleOrDefault(n => n.idStudent.CompareTo(idStudent) == 0);
            if (sv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            try
            {
                db.Students.DeleteOnSubmit(sv);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Loi = "Không thể xóa, có dữ liệu từ bảng con đang sử dụng dữ liệu này, vui lòng kiểm tra lại!";
                return this.Delete(idStudent);
            }

        }
    }
}