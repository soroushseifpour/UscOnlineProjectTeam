using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using UscProject.Areas.KarFarma.ViewModel;
using UscProject.Models;

namespace UscProject.Areas.KarFarma.Controllers
{
    [Authorize(Roles = "Karfarma")]
    public class PanelController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: KarFarma/Panel
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(p => p.UserName == username).FirstOrDefault();
            int employeeid = db.EmployeeTB.Where(p => p.UserID == user.UserID).FirstOrDefault().EmployeeID;
            ViewBag.CompanyName = db.EmployeeTB.Where(o => o.EmployeeID == employeeid).First().CompanyName;
            int userid = db.EmployeeTB.Where(o => o.EmployeeID == employeeid).First().UserID;
            var model = db.UserTB.Find(userid);
            ViewBag.Email = db.UserTB.Where(o => o.UserID == userid).First().Email;
            ViewBag.ResumeSent = db.TheResumeForEmployees(employeeid).Count();
            ViewBag.Forms = db.TheTotallFormsOfKarfarma(employeeid).Count();
            return View(model);
        }
        public ActionResult Header()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(t => t.UserName == username).FirstOrDefault();
            var model = db.UserTB.Where(o => o.UserID == user.UserID).First();
            return PartialView(model);

        }
        [HttpGet]
        public ActionResult PersonalInformation()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(p => p.UserName == username).FirstOrDefault();
            var model = db.UserTB.Where(o => o.UserID == user.UserID).First();
            ViewBag.CompanyName = db.EmployeeTB.Where(p => p.UserID == user.UserID).First().CompanyName;
            return View(model);
        }

        [HttpPost]
        public ActionResult PersonalInformation(UserTB user, string companyname, HttpPostedFileBase ImgUp)
        {
            //var picStatus = user.ImageName;
            //if (picStatus == false)
            //{
            //    ImgUp = null;
            //}
            //else 
            //{
            //    user.PictureName=
            //}

            if (ImgUp != null)
            {
                user.ImageName = true;
                if (user.PictureName != "image.png")
                {
                    System.IO.File.Delete(Server.MapPath("/Files/UserPictures/Custom/Karfarma/" + user.PictureName));
                }

                user.PictureName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                ImgUp.SaveAs(Server.MapPath("/Files/UserPictures/Custom/Karfarma/" + user.PictureName));


            }
            var employee = db.EmployeeTB.Where(p => p.UserID == user.UserID).First();
            employee.CompanyName = companyname;
            db.Entry(employee).State = EntityState.Modified;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.CompanyName = companyname;
            return View(user);
        }

        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}
