using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;
using UscProject.ViewModel;

namespace UscProject.Controllers
{
    public class JobController : Controller
    {
        // GET: Job

        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();

        [Route("JobLists/{id}")]
        public ViewResult JobLists(int? id, int? page)
        {

            var forms = db.TheTotallFormsOfKarfarma(id).ToList();
            List<JobListsvm> JobLists = new List<JobListsvm>();
            foreach (var item in forms)
            {
                var j = new JobListsvm();
                j.jobs = item;
                var c = db.JobCategoryTB.Where(e => e.JobID == item.JobID).FirstOrDefault().JobCategory;
                j.jobcat = c;
                JobLists.Add(j);
            }
            
            ViewBag.id = id;

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(JobLists.ToPagedList(pageNumber, pageSize));


        }

        [Route("JobDetails/{id}")]
        public ActionResult JobDetails(int id)
        {
            var form = db.FormTB.Find(id);
            int employeeid = form.OrderDetailTB.EmployeeID;
            var user = db.EmployeeTB.Where(e => e.EmployeeID == employeeid).FirstOrDefault().UserTB;
            string img;
            if (user.ImageName == true)
            {
                img = Url.Content("/Files/UserPictures/Custom/KarFarma/" + user.PictureName);
            }
            else
            {
                img = Url.Content("/Files/UserPictures/Default/UserProfile.jpg");
            }
            ViewBag.img = img;
            ViewBag.company = db.FormDetailTB.Where(d => d.FormID == id).FirstOrDefault().EmployeeTB.CompanyName;
            ViewBag.email = db.FormDetailTB.Where(d => d.FormID == id).FirstOrDefault().EmployeeTB.UserTB.Email;
            ViewBag.address = db.FormDetailTB.Where(d => d.FormID == id).FirstOrDefault().EmployeeTB.Adress;
            ViewBag.site = db.FormDetailTB.Where(d => d.FormID == id).FirstOrDefault().EmployeeTB.Site;
            return View(form);
        }
        [HttpPost]
        public JsonResult AddForm(int FormID)
        {
            var name = User.Identity.Name;
            var Role = db.UserTB.Where(d => d.UserName == name).FirstOrDefault().RoleID;
            var user = db.UserTB.Where(d => d.UserName == name).FirstOrDefault().UserID;
            if (Role == 1) 
            {
                return Json(new JsonData()
                {
                    Status = false,
                    Message="شما ادمین هستید!!!!"
                });
            }
            if (Role == 2) 
            {
                return Json(new JsonData()
                {
                    Status = false,
                    Message="شما به عنوان کارفرما حق ارسال رزومه ندارید!!!"
                });
            }
             var resumeid = db.EmployerTB.Where(e => e.UserID == user).First().ResumeID;
            if (resumeid == null)
            {
                return Json(new JsonData()
                {
                    Status = false,
                    Message="ابتدا رزومه خود را بسازید"
                });
            }
            else
            {
                var re = new ResumeEmployeeTB();
                var employer = db.EmployerTB.Where(e => e.UserID == user).First();
                var exists = db.ResumeEmployeeTB.Where(o => o.ResumeID == employer.ResumeID && o.FormID == FormID).Single();
                if (exists != null) 
                {
                    return Json(new JsonData()
                    {
                        Status = false,
                        Message = "شما قبلا برای این شغل رزومه ارسال کرده اید!!!"
                    }); 
                }
                re.FormID = FormID;
                re.ResumeID = employer.ResumeID;
                re.Date = DateTime.Now;
                db.ResumeEmployeeTB.Add(re);
                db.SaveChanges();
                return Json(new JsonData()
                {
                    Status = true
                });
            }

        }
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}