using E_Learning_Prototype.Entities;
using E_Learning_Prototype.infrastructure;
using E_Learning_Prototype.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace E_Learning_Prototype.Controllers
{
    [Authorize]
    public class DevoirController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Devoir
        public ActionResult Index(string Code)
        {

            if (checkCode(Code) == null)
            {
                return RedirectToAction("Index", "Course", null);
            }

            ViewBag.Code = Code;
            string userid = User.Identity.GetUserId();


            var model = _db.Devoirs.Where(x => x.Course.Code == Code && x.DateLimite>DateTime.Now)
                                   .Select(x => new DevoirViewModel
                                   {
                                       Assigned = x.Course.Course_Students.Count - x.Render_Devoirs.Count,
                                       HandedIn = x.Render_Devoirs.Count,
                                       instructions = x.instructions,
                                       Titre = x.Titre,
                                       Attachements = x.Attachements,
                                       Id_Devoir = x.Id,
                                       Done = x.Render_Devoirs.FirstOrDefault(xx => xx.Student.Id == userid) != null,
                                       DateLimite=x.DateLimite,
                                       Theme=x.Theme.Nom
                                   });
            return View(model);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult AddDevoir(string Code)
        {
            Session["Attachement"] = new List<Attachement>();
            if (checkCode(Code) == null)
            {
                return RedirectToAction("Index", "Course", null);
            }

            ViewBag.themes = new SelectList(_db.Themes,"Id", "Nom");

            AddDevoirViewModel model = new AddDevoirViewModel
            {
                CodeCourse = Code,
                DateLimite = DateTime.Now
            };
            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult AddDevoir(AddDevoirViewModel model)
        {
            Course c = checkCode(model.CodeCourse);

            if (c != null)
            {

                if (model.DateLimite<=DateTime.Now)
                {
                    Session["Attachement"] = new List<Attachement>();
                    ModelState.AddModelError(string.Empty, "la date doit être supérieure à la date du jour");
                    ViewBag.themes = new SelectList(_db.Themes, "Id", "Nom");
                    return View(model);
                }

                Teacher t = _db.Teachers.Find(User.Identity.GetUserId());

                Devoir D = new Devoir
                {
                    DateLimite = model.DateLimite,
                    Titre = model.Titre,
                    instructions = model.instructions,
                    Points = model.Points,
                    Course = c,
                    Teacher = t,
                    Theme = _db.Themes.Find(model.Theme_id),
                    Attachements = (Session["Attachement"] as List<Attachement>),
                };

                _db.Devoirs.Add(D);


                _db.SaveChanges();

                return RedirectToAction("Index", "Devoir", new { Code = c.Code });
            }
            return RedirectToAction("Index", "Course", null);
        }



        [Authorize(Roles = "Student")]
        [HttpGet]
        public ActionResult RendreDevoir(int id_devoire)
        {
            ViewBag.CodeDevoir = _db.Devoirs.Find(id_devoire).Course.Code;
            Session["Attachement"] = new List<Attachement>();
            string userid = User.Identity.GetUserId();
            RendreDevoirViewModel rd = new RendreDevoirViewModel
            {
                Devoir = _db.Devoirs.Find(id_devoire),
                Render_devoir = _db.Render_Devoirs
                    .FirstOrDefault(x => x.Devoir.Id == id_devoire && x.Student.Id == userid)
            };

            return View(rd);
        }




        [Authorize(Roles = "Student")]
        [HttpPost]
        public ActionResult RendreDevoir(RendreDevoirViewModel model)
        {
            Render_Devoir res = new Render_Devoir
            {
                DateRmise = DateTime.Now,
                Devoir = _db.Devoirs.Find(model.Devoir.Id),
                Student = _db.Students.Find(User.Identity.GetUserId()),
                Attachements = (Session["Attachement"] as List<Attachement>)
            };

            _db.Render_Devoirs.Add(res);

            _db.SaveChanges();

            return RedirectToAction("Index", new { Code = res.Devoir.Course.Code });
        }


        public ActionResult DetailsDevoir(int id_devoire)
        {
            ViewBag.CodeDevoir = _db.Devoirs.Find(id_devoire).Course.Code;

            DetailDevoirModelView model = new DetailDevoirModelView
            {
                remis = _db.Devoirs.Find(id_devoire).Render_Devoirs
                .Select(x => new DetailDevoirModel
                {
                    Id = x.Id,
                    First_Name = x.Student.User.First_Name,
                    Last_Name = x.Student.User.Last_Name,
                    Date_Remis = x.DateRmise,
                    Note=x.Note
                }),

                NoRemis = _db.Devoirs.Find(id_devoire).Course.Course_Students
                .Where(x => x.Student.Render_Devoirs.FirstOrDefault(xx=>xx.Devoir.Id==id_devoire) == null)
                .Select(x =>
                new DetailDevoirModel
                {
                    Id = 0,
                    Note=0,
                    First_Name = x.Student.User.First_Name,
                    Last_Name = x.Student.User.Last_Name,
                    Date_Remis = DateTime.Now
                })
            };
            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult MarkDevoire(int Id_rendreDevoire)
        {
            ViewBag.CodeDevoir = _db.Render_Devoirs.Find(Id_rendreDevoire).Devoir.Course.Code;
            Render_Devoir rd = _db.Render_Devoirs.Find(Id_rendreDevoire);
            MarkDevoirViewModel model = new MarkDevoirViewModel
            {
                Id_rendreDevoire = Id_rendreDevoire,
                Attachements = rd.Attachements,
                FullName = rd.Student.User.First_Name + " " + rd.Student.User.Last_Name,
                DevoirNote = rd.Devoir.Points,
                Note = rd.Note
            };
            return View(model);
        }


        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult MarkDevoire(MarkDevoirViewModel model)
        {
            if (_db.Render_Devoirs.Find(model.Id_rendreDevoire).Devoir.Teacher.Id != User.Identity.GetUserId()
                && model.Note >= model.DevoirNote)
            {
                return View(model);
            }


            _db.Render_Devoirs.Find(model.Id_rendreDevoire).Note = model.Note;

            _db.SaveChanges();

            Devoir d = _db.Render_Devoirs.Find(model.Id_rendreDevoire).Devoir;

            return RedirectToAction("DetailsDevoir", new { id_devoire = d.Id });

        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult DeleteDevoir(int Id_Devoir)
        {
            Devoir dev = _db.Devoirs.Find(Id_Devoir);

            string CodeCourse = dev.Course.Code;

            if (dev.Teacher.Id!=User.Identity.GetUserId())
            {
                return RedirectToAction("Index","Course");
            }
            foreach (var item in dev.Render_Devoirs)
            {
                _db.Attachements.RemoveRange(item.Attachements);
            }
            _db.Render_Devoirs.RemoveRange(dev.Render_Devoirs);
            _db.Attachements.RemoveRange(dev.Attachements);
            _db.Devoirs.Remove(dev);
            _db.SaveChanges();

            return RedirectToAction("Index",new {Code = CodeCourse });

        }

        public ActionResult SaveFiles()
        {
            List<string> filesNames = new List<string>();
            foreach (string FileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[FileName];
                filesNames.Add(file.FileName);
                String pathOnServer = Path.Combine(Path.Combine(HostingEnvironment.MapPath("~/Upload/")));

                if (!Directory.Exists(pathOnServer))
                {
                    Directory.CreateDirectory(pathOnServer);
                }

                var fullPath = Path.Combine(pathOnServer, Path.GetFileName(file.FileName));

                Attachement a = new Attachement
                {
                    Path = fullPath,
                    FileName = Path.GetFileName(file.FileName),
                    Type = file.ContentType,
                    url = "",
                };

                _db.Attachements.Add(a);
                //_db.SaveChanges();
                (Session["Attachement"] as List<Attachement>).Add(a);


                file.SaveAs(fullPath);
            }

            return Json(new { response = filesNames }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveLink(string Link)
        {

            Attachement a = new Attachement
            {
                Path = "",
                FileName = "",
                Type = "Url",
                url = Link,
            };

            _db.Attachements.Add(a);

            (Session["Attachement"] as List<Attachement>).Add(a);

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        private Course checkCode(string Code)
        {
            Course c = _db.Courses.FirstOrDefault(x => x.Code == Code);
            return c;
        }

        public FileResult Download(int FileID)
        {
            Attachement att = _db.Attachements.Find(FileID);
            //string pathOnServer = Path.Combine(Path.Combine(HostingEnvironment.MapPath("~/Upload/")));

            var fullPath = att.Path;

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, att.FileName);
        }

    }
}