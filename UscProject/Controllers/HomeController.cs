using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;
using UscProject.ViewModel;
using PagedList;
using System.Web.Razor.Generator;
using Microsoft.Ajax.Utilities;

namespace UscProject.Controllers
{
    public class HomeController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        public ActionResult Index()
        {
            ViewBag.company = db.EmployeeTB.Count();
            ViewBag.Resume = db.ResumeTB.Count();
            ViewBag.User = db.UserTB.Count() - 1;
            ViewBag.Forms = db.FormTB.Count();

            var employees = db.TheTop5Employee;
            var model = new Indexvm();
            var companies = new List<Companiesvm>();
            foreach (var item in employees)
            {
                var c = new Companiesvm();
                var user = db.UserTB.Where(u => u.UserID == item.UserID).FirstOrDefault();
                if (user.ImageName==false)
                {
                    c.img = Url.Content("/Files/UserPictures/Default/UserProfile.jpg");
                }
                else 
                {
                    c.img= Url.Content("/Files/UserPictures/Custom/Karfarma/" + user.PictureName);
                }
                int count = db.TheTotallFormsOfKarfarma(item.EmployeeID).Count();
                c.FormsCount = count;
                c.address = item.Adress;
                c.name = item.CompanyName;
                c.id = item.EmployeeID;
                companies.Add(c);
            }
            model.companiesvms = companies;
            var boxes = db.BoxCategory.ToList();
            model.boxes = boxes;
            var jobes = new List<Categoryvm>();
            var jobcategories = db.JobCategoryTB.ToList();
            foreach (var item in jobcategories)
            {
                var job = new Categoryvm();
                int count = db.FormTB.Where(u => u.JobID == item.JobID).Count();
                job.catname = item.JobCategory;
                job.count = count;
                jobes.Add(job);
            }
            model.categoryvms = jobes;
            return View(model);
        }

        [Route("Companies")]
        public ViewResult Companies(int? page)
        {
            List<Companiesvm> Companies = new List<Companiesvm>();
            var employee = db.EmployeeTB.ToList();
            foreach (var item in employee)
            {
                var comp = new Companiesvm();
                var userid = db.EmployeeTB.Where(e => e.EmployeeID == item.EmployeeID).FirstOrDefault().UserID;
                var user = db.UserTB.Find(userid);
                comp.id = item.EmployeeID;
                comp.name = item.CompanyName;
                comp.address = item.Adress;
                if (user.ImageName == true)
                {
                    comp.img = Url.Content("/Files/UserPictures/Custom/KarFarma/" + user.PictureName);
                }
                else
                {
                    comp.img = Url.Content("/Files/UserPictures/Default/UserProfile.jpg");
                }
                comp.FormsCount = db.TheTotallFormsOfKarfarma(item.EmployeeID).Count();
                Companies.Add(comp);
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(Companies.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult BlogDetails() 
        {
            var cat = db.JobCategoryTB.ToList();
            List<Categoryvm> categoryvms = new List<Categoryvm>();
            foreach (var item in cat)
            {
                var c = new Categoryvm();
                int forms = db.FormTB.Where(a => a.JobCategoryTB.JobCategory == item.JobCategory).Count();
                c.catname = item.JobCategory;
                c.count = forms;
                categoryvms.Add(c);
            }
            return View(categoryvms);
        }
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}