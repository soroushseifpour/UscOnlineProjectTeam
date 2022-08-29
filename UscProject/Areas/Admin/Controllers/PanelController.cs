using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;
using Newtonsoft.Json;
using UscProject.Areas.Admin.ViewModels;
using System.Data.Entity.Core.Objects;
using Microsoft.Ajax.Utilities;
using System.Data.Entity;
using System.IO;
using UscProject.Help;
using System.Net.Mail;
using System.Net;
using IdentityServer4.Models;
using DocumentFormat.OpenXml.Vml.Office;
using System.Web.Security;

namespace UscProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        // GET: Admin/Panel
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();

        public ActionResult Header()
        {
            var user = db.UserTB.Find(1);
            return PartialView(user);
        }

        public ActionResult Index()
        {
            ViewBag.resume = db.ResumeTB.Count();
            ViewBag.forms = db.FormTB.Count();
            ViewBag.karjo = db.EmployeeTB.Count();
            ViewBag.karfarma = db.EmployerTB.Count();

            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();
            for (int i = DateTime.Now.Year; i >= 2018; i--)
            {
                var Gold = db.OrderDetailTB.Where(p => p.BuyDate.Value.Year == i && p.BoxCategoryID == 1).Sum(p => (int?)p.UnitPrice) ?? 0;
                var Silver = db.OrderDetailTB.Where(p => p.BuyDate.Value.Year == i && p.BoxCategoryID == 2).Sum(p => (int?)p.UnitPrice) ?? 0;
                var noghre = db.OrderDetailTB.Where(p => p.BuyDate.Value.Year == i && p.BoxCategoryID == 3).Sum(p => (int?)p.UnitPrice) ?? 0;

                DataPoint data1 = new DataPoint(i.ToString(), Convert.ToDouble(Gold));
                dataPoints1.Add(data1);


                DataPoint data2 = new DataPoint(i.ToString(), Convert.ToDouble(Silver));
                dataPoints2.Add(data2);


                DataPoint data3 = new DataPoint(i.ToString(), Convert.ToDouble(noghre));
                dataPoints3.Add(data3);

            }

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);
            var model = db.BoxCategory.ToList();
            return View(model);
        }


        public ActionResult PersonalInformation()
        {

            var model = db.UserTB.Find(1);
            if (model.ImageName)
            {
                ViewBag.PicStatus = true;
            }
            else
            {
                ViewBag.PicStatus = false;
            }
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
                    System.IO.File.Delete(Server.MapPath("/Files/UserPictures/Custom/" + user.PictureName));
                }

                user.PictureName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                ImgUp.SaveAs(Server.MapPath("/Files/UserPictures/Custom/" + user.PictureName));


            }

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return View(user);
        }


        [Route("LogOut")]
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}