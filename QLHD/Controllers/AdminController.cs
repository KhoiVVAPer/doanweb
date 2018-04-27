using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLHD.Models;
using QLHD.ViewModel;

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
            
            return View(db.Events.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.idType = new SelectList(db.EventTypes.ToList().OrderBy(n => n.typename), "idEventType", "typename");
            ViewBag.idOganizer = new SelectList(db.Oganizers.ToList().OrderBy(n => n.name), "idOganizer", "name");
            ViewBag.idSemester = new SelectList(db.Semesters.ToList().OrderBy(n => n.name), "idSemester", "name");
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name");
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Event ev,FormCollection colle)
        {
            try
            {
                ViewBag.idType = new SelectList(db.EventTypes.ToList().OrderBy(n => n.typename), "idEventType", "typename");
                ViewBag.idOganizer = new SelectList(db.Oganizers.ToList().OrderBy(n => n.name), "idOganizer", "name");
                ViewBag.idSemester = new SelectList(db.Semesters.ToList().OrderBy(n => n.name), "idSemester", "name");
                ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name");
                ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name");

                var mahd = colle["idEvent"];
                var tenhd = colle["eventName"];
                var nd = colle["content"];
                var ngaydangky = String.Format("{0:MM/dd/yyyy}", colle["dateRegister"]);
                var ngayktdangky = String.Format("{0:MM/dd/yyyy}", colle["dateEndRegister"]);
                var ngaydienra = String.Format("{0:MM/dd/yyyy}", colle["dateStart"]);
                var ngayketthuc = String.Format("{0:MM/dd/yyyy}", colle["dateEnd"]);
                var loaihd = int.Parse(colle["idType"]);
                var dvtochuc = int.Parse(colle["idOganizer"]);
                var hocky = int.Parse(colle["idSemester"]);
                var khoa = int.Parse(colle["idFalcuty"]);
                var lop = int.Parse(colle["idClass"]);
                var sldangky = int.Parse(colle["studentNumber"]);
                var diadiem = colle["address"];

                ev.eventName = tenhd;
                ev.content = nd;
                ev.dateRegister = DateTime.Parse(ngaydangky);
                ev.dateEndRegister = DateTime.Parse(ngayktdangky);
                ev.dateStart = DateTime.Parse(ngaydienra);
                ev.dateEnd = DateTime.Parse(ngayketthuc);
                ev.idEventType = loaihd;
                ev.idOganizer = dvtochuc;
                ev.idSemester = hocky;
                ev.idFalcuty = khoa;
                ev.idClass = lop;
                ev.studentNumber = sldangky;
                ev.address = diadiem;

                db.Events.InsertOnSubmit(ev);
                db.SubmitChanges();
                return RedirectToAction("DSHD");
            }
            catch
            {
                ViewData["Error"] = "Đã xảy ra lỗi, vui lòng kiểm tra lại";
                return this.Create();
            }
        }

        int d;
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Event ev = db.Events.SingleOrDefault(n => n.idEvent == id);
            if (ev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idType = new SelectList(db.EventTypes.ToList().OrderBy(n => n.typename), "idEventType", "typename", ev.idEventType);
            ViewBag.idOganizer = new SelectList(db.Oganizers.ToList().OrderBy(n => n.name), "idOganizer", "name", ev.idOganizer);
            ViewBag.idSemester = new SelectList(db.Semesters.ToList().OrderBy(n => n.name), "idSemester", "name", ev.idSemester);
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name", ev.idFalcuty);
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name", ev.idClass);
            return View(ev);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id,FormCollection colle)
        {
            ViewBag.idType = new SelectList(db.EventTypes.ToList().OrderBy(n => n.typename), "idEventType", "typename");
            ViewBag.idOganizer = new SelectList(db.Oganizers.ToList().OrderBy(n => n.name), "idOganizer", "name");
            ViewBag.idSemester = new SelectList(db.Semesters.ToList().OrderBy(n => n.name), "idSemester", "name");
            ViewBag.idFalcuty = new SelectList(db.Falcuties.ToList().OrderBy(n => n.name), "idFalcuty", "name");
            ViewBag.idClass = new SelectList(db.Classes.ToList().OrderBy(n => n.name), "idClass", "name");

            var mahd = colle["idEvent"];
            var tenhd = colle["eventName"];
            var nd = colle["content"];
            var ngaydangky = String.Format("{0:MM/dd/yyyy}", colle["dateRegister"]);
            var ngayktdangky = String.Format("{0:MM/dd/yyyy}", colle["dateEndRegister"]);
            var ngaydienra = String.Format("{0:MM/dd/yyyy}", colle["dateStart"]);
            var ngayketthuc = String.Format("{0:MM/dd/yyyy}", colle["dateEnd"]);
            var loaihd = int.Parse(colle["idType"]);
            var dvtochuc = int.Parse(colle["idOganizer"]);
            var hocky = int.Parse(colle["idSemester"]);
            var khoa = int.Parse(colle["idFalcuty"]);
            var lop = int.Parse(colle["idClass"]);
            var sldangky = int.Parse(colle["studentNumber"]);
            int sldadangky;
            try
            {
                sldadangky = int.Parse(colle["Registation"]);
            }
            catch
            {
                sldadangky = 0;
            }
            int sldathamgia;
            try
            {
                sldathamgia = int.Parse(colle["Registation"]);
            }
            catch
            {
                sldathamgia = 0;
            }
           
            var diadiem = colle["address"];

            var ev = db.Events.First(n => n.idEvent == int.Parse(mahd));

            ev.eventName = tenhd;
            ev.content = nd;
            ev.dateRegister = DateTime.Parse(ngaydangky);
            ev.dateEndRegister = DateTime.Parse(ngayktdangky);
            ev.dateStart= DateTime.Parse(ngaydienra);
            ev.dateEnd= DateTime.Parse(ngayketthuc);
            ev.idEventType = loaihd;
            ev.idOganizer = dvtochuc;
            ev.idSemester = hocky;
            ev.idFalcuty = khoa;
            ev.idClass = lop;
            ev.studentNumber = sldangky;
            ev.Registation = sldadangky;
            ev.Attendant = sldathamgia;
            ev.address = diadiem;

            UpdateModel(ev);
            db.SubmitChanges();


            return RedirectToAction("Edit");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Event ev = db.Events.SingleOrDefault(n => n.idEvent == id);
            ViewBag.MaHD = ev.idEvent;
            if (ev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
          
            return View(ev);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Event ev = db.Events.SingleOrDefault(n => n.idEvent == id);
            ViewBag.MaHD = ev.idEvent;
            if (ev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
 
            return View(ev);
            
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult Deleted(int id)
        {
            Event ev = db.Events.SingleOrDefault(n => n.idEvent == id);
            ViewBag.MaHD = ev.idEvent;
            if (ev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Events.DeleteOnSubmit(ev);
            db.SubmitChanges();
            return RedirectToAction("DSHD");

        }
        public ActionResult DSSVTG(int idEvent)
        {
            //var sv = db.EventStudentLists.Where(n => n.idEvent == idEvent);
            var model = from a in db.Students
                       join b in db.EventStudentLists on a.idStudent equals b.idStudent
                        join c in db.Classes on a.idClass equals c.idClass
                        join d in db.Falcuties on a.idFalcuty equals d.idFalcuty
                        where b.idEvent == idEvent
                        select new DSSVTGViewModel()
                        {
                            id = a.idStudent,
                            name = a.name,
                            dob = a.dob,
                            lop = c.name,
                            khoa = d.name,
                            diachi = a.andress,
                            email = a.email,
                            sdt = a.phone,
                            tg = b.FalcutyConfirmed.Value

                        };
            return View(model.ToList());
        }
        public static string id2;
        [HttpGet]
        public ActionResult EditSV(string idStudent)
        {
            EventStudentList stev = db.EventStudentLists.SingleOrDefault(n => n.idStudent == idStudent);
            if (stev == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idEvent = new SelectList(db.Events.ToList().OrderBy(n => n.eventName), "idEvent", "eventName", stev.idEvent);
            id2 = idStudent;
            return View(stev);
        }

        public static bool xn;

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSV(FormCollection colle)
        {
           
            ViewBag.idEvent = new SelectList(db.Events.ToList().OrderBy(n => n.eventName), "idEvent", "eventName");
            var hd = int.Parse(colle["idEvent"]);
            var id = int.Parse(colle["id"]);
            var idtd = colle["idStudent"];
         
           // var xn2 = colle["FalcutyConfirmed.Value"];
          
            var stev =db.EventStudentLists.First(n => n.id == id);

            var xn1 = bool.Parse(colle["chon"]);
            stev.FalcutyConfirmed = xn1;
            stev.idEvent = hd;
            

            UpdateModel(stev);
            db.SubmitChanges();
            return this.EditSV(idtd);
        }
    }
}