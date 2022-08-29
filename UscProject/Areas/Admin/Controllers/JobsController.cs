using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;

namespace UscProject.Areas.Admin.Controllers
{
    public class JobsController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: Admin/Jobs
        public ActionResult JobCategory()
        {
            var model = db.JobCategoryTB.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult EditJob(int id)
        {
            var model = db.JobCategoryTB.Find(id);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult EditJob(int JobID, string JobCategory)
        {
            var job = db.JobCategoryTB.Find(JobID);
            job.JobCategory = JobCategory;

            db.SaveChanges();
            return Redirect("JobCategory");
        }
        public void DeleteJob(int id)
        {
            var j = db.JobCategoryTB.Find(id);
            db.Entry(j).State = EntityState.Deleted;
            db.SaveChanges();
        }
        public ActionResult AddJob()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddJob(string JobCategory)
        {

            var model = new JobCategoryTB();
            model.JobCategory = JobCategory;
            db.JobCategoryTB.Add(model);
            db.SaveChanges();
            return Redirect("JobCategory");
        }
    }
}