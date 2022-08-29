using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.KarJo.ViewModel;
using UscProject.Models;

namespace UscProject.Areas.KarJo.Controllers
{
    public class PanelController : Controller
    {
        // GET: KarJo/Panel

        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        public ActionResult Header()
        {
            var username = User.Identity.Name;
            var model = db.UserTB.Where(u=>u.UserName==username).SingleOrDefault();
            return PartialView(model);
        }
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var model = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            int userid = model.UserID;
            int emloyerid = db.EmployerTB.Where(e => e.UserID == userid).First().EmployerID;
            int? resumeid = db.EmployerTB.Find(emloyerid).ResumeID;
            var sentforms = db.TheFormsOFEachResume(resumeid).Count();
            ViewBag.sentforms = sentforms;
            ViewBag.resume = resumeid;
            return View(model);
        }
        public ActionResult PersonalInformation()
        {
            var username = User.Identity.Name;
            var model = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult PersonalInformation(UserTB user, HttpPostedFileBase ImgUp)
        {
            if (ImgUp != null)
            {
                user.ImageName = true;
                if (user.PictureName != "image.png")
                {
                    System.IO.File.Delete(Server.MapPath("/Files/UserPictures/Custom/KarJo/" + user.PictureName));
                }

                user.PictureName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                ImgUp.SaveAs(Server.MapPath("/Files/UserPictures/Custom/KarJo/" + user.PictureName));


            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return View(user);
        }
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}