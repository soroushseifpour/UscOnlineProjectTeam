using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;

namespace UscProject.Areas.KarJo.Controllers
{
    public class ResumeController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: KarJo/Resume
        public ActionResult EditResume()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            int userid = user.UserID;
            var employer = db.EmployerTB.Where(o => o.UserID == userid).First();
            var resume = db.ResumeTB.Where(e => e.ResumeID == employer.ResumeID).First();
            if (user.ImageName == false)
            {
                ViewBag.path = Url.Content("/Files/UserPictures/Default/UserProfile.jpg");
            }
            else
            {
                ViewBag.path = Url.Content("/Files/UserPictures/Custom/KarJo" + user.PictureName);
            }
            return View(resume);
        }
        [HttpPost]
        public JsonResult EditResume(ResumeTB resume)
        {
            db.Entry(resume).State = EntityState.Modified;

            db.SaveChanges();
            return Json(new JsonData()
            {
                Status = true
            });
        }
        public ActionResult NewResume()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            int userid = user.UserID;
            var emloyer = db.EmployerTB.Where(e => e.UserID == userid).First();
            if (emloyer.ResumeID == null)
            {
                return View();
            }
            else
            {
                var resume = db.ResumeTB.Find(emloyer.ResumeID);
                return RedirectToAction("EditResume");
            }

        }
        [HttpPost]
        public JsonResult NewResume(ResumeTB resume)
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            int userid = user.UserID;
            var employer = db.EmployerTB.Where(e => e.UserID == userid).First();
            resume.RequestDate = DateTime.Today;
            db.Entry(resume).State = EntityState.Added;
            employer.ResumeID = resume.ResumeID;
            db.SaveChanges();
            return Json(new JsonData()
            {
                Status = true
            });
        }
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}