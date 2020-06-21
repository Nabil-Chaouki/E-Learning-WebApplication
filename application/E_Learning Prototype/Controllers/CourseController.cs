using E_Learning_Prototype.Entities;
using E_Learning_Prototype.infrastructure;
using E_Learning_Prototype.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Learning_Prototype.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Course
        public ActionResult Index()
        {
            ViewBag.IsinRoleTeacher = User.IsInRole("Teacher");

            string userID = User.Identity.GetUserId();

            CourseViewModel c = new CourseViewModel();

            if (User.IsInRole("Teacher"))
            {
                Teacher t = _db.Teachers.Find(userID);
                c.courses = t.Courses
                            .Select(x => new CourseModel
                            {
                                Nom = x.Nom,
                                Description = x.Description,
                                StudentsCount = x.Course_Students.Count,
                                CodeCourse = x.Code
                            });
            }
            else
            {
                Student s = _db.Students.Find(userID);
                c.courses = s.Course_Students.Select(x => new CourseModel
                {
                    Nom = x.Course.Nom,
                    Description = x.Course.Description,
                    StudentsCount = x.Course.Course_Students.Count,
                    CodeCourse = x.Course.Code
                });
            }

            return View(c);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course c = new Course
                {
                    Nom = model.Course.Nom,
                    Description = model.Course.Description,
                    Salle = model.Course.Salle,
                    Section = model.Course.Section,
                    Code = Guid.NewGuid().ToString("N"),
                    Teacher = _db.Teachers.Find(User.Identity.GetUserId())
                };

                _db.Courses.Add(c);

                _db.SaveChanges();

                return RedirectToAction("index");
            }

            return View("index", model);
        }

        [Authorize(Roles = "Student")]
        public ActionResult AddCourseToStudent(string code)
        {
            Course C = _db.Courses.FirstOrDefault(x => x.Code == code);
            if (C != null)
            {
                Student s = _db.Students.Find(User.Identity.GetUserId());
                s.Course_Students.Add(new Course_Student { Course = C });
                _db.SaveChanges();

                //return RedirectToAction("Index","Stream",code);

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteCourse(string CourseCode)
        {
            Course c = _db.Courses.FirstOrDefault(x=>x.Code==CourseCode);

            _db.Course_Students.RemoveRange(c.Course_Students);

            foreach (var item in c.Devoirs)
            {
                foreach (var devR in item.Render_Devoirs)
                {
                    _db.Attachements.RemoveRange(devR.Attachements);
                }
                _db.Render_Devoirs.RemoveRange(item.Render_Devoirs);
                _db.Attachements.RemoveRange(item.Attachements);
            }
            _db.Devoirs.RemoveRange(c.Devoirs);
            foreach (var item in c.Informations)
            {
                _db.Attachements.RemoveRange(item.Attachements);
            }
            _db.Informations.RemoveRange(c.Informations);

            _db.Courses.Remove(c);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}