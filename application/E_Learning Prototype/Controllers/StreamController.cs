using E_Learning_Prototype.Entities;
using E_Learning_Prototype.infrastructure;
using E_Learning_Prototype.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace E_Learning_Prototype.Controllers
{
    [Authorize]
    public class StreamController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        // GET: Stream
        public ActionResult Index(string Code)
        {
            string userID = User.Identity.GetUserId();

            //restrict join class from url
            Course c = _db.Courses.FirstOrDefault(x => (x.Code == Code) &&
                                                     (x.Teacher.Id == userID || x.Course_Students.FirstOrDefault(xx => xx.Id_student == userID) != null));
            if (c == null)
            {
                return RedirectToAction("Index", "Course", null);
            }


            Session["Attachement"] = new List<Attachement>();

            var LatestDatesPerDay = _db.Informations.Where(x => x.Course.Code == Code)
                .GroupBy(x => DbFunctions.TruncateTime(x.Publish_Date))
                    .Select(x => x.Max(xx => xx.Publish_Date)).ToList();

            InfomationViewModel model = new InfomationViewModel
            {
                infos = _db.Informations.Where(x => x.Course.Code == Code)
                        .OrderBy(x => x.Publish_Date).ToList(),
                LatestDatesPerDay = LatestDatesPerDay,
                Course_code = Code,
                Classname = c.Nom,
                ClassSubject = c.Section
            };


            return View(model);
        }


        [HttpPost]
        public ActionResult Index(InfomationViewModel model)
        {

            Information info = new Information
            {
                Message = model.Message,
                Object = "",
                Publish_Date = DateTime.Now,
                user = _db.Users.Find(User.Identity.GetUserId()),
                Attachements = (Session["Attachement"] as List<Attachement>),
                Course = _db.Courses.FirstOrDefault(x => x.Code == model.Course_code)
            };

            _db.Informations.Add(info);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public FileResult Download(int FileID)
        {
            Attachement att = _db.Attachements.Find(FileID);
            //string pathOnServer = Path.Combine(Path.Combine(HostingEnvironment.MapPath("~/Upload/")));

            var fullPath = att.Path;

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, att.FileName);
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

                var fullPath = Path.Combine(pathOnServer, Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName) );
                
                Attachement a = new Attachement
                {
                    Path = fullPath,
                    FileName = Path.GetFileName(file.FileName),
                    Type = file.ContentType,
                    url = "",
                };


                _db.Attachements.Add(a);

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

    }
}