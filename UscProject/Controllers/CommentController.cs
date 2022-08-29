using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;
using UscProject.ViewModel;
namespace UscProject.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment

        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();

       
        public ActionResult Index()
        {

            var cat = db.JobCategoryTB.ToList();
            var commentvm = new Commentvm();
            List<Categoryvm> categoryvms = new List<Categoryvm>();
            foreach (var item in cat)
            {
                var c = new Categoryvm();
                int forms = db.FormTB.Where(a => a.JobCategoryTB.JobCategory == item.JobCategory).Count();
                c.catname = item.JobCategory;
                c.count = forms;
                categoryvms.Add(c);
            }
            commentvm.categoryvms = categoryvms;
            return View(commentvm);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string CommentUserName, string CommentEmail, string CommentText) 
        {
            var c = new CommentTB()
            {
                CommentEmail = CommentEmail,
                CommentUserName = CommentUserName,
                CommentText = CommentText,
                CommentDate = DateTime.Today
            };
            db.CommentTB.Add(c);
            db.SaveChanges();
            var cat = db.JobCategoryTB.ToList();
            var commentvm = new Commentvm();
            List<Categoryvm> categoryvms = new List<Categoryvm>();
            foreach (var item in cat)
            {
                var s = new Categoryvm();
                int forms = db.FormTB.Where(a => a.JobCategoryTB.JobCategory == item.JobCategory).Count();
                s.catname = item.JobCategory;
                s.count = forms;
                categoryvms.Add(s);
            }
            commentvm.categoryvms = categoryvms;
            return View(commentvm);
        }
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}