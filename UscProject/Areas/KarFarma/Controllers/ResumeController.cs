using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.KarFarma.ViewModel;
using UscProject.Models;

namespace UscProject.Areas.KarFarma.Controllers
{
    public class ResumeController : Controller
    {
        // GET: KarFarma/Resume
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        [HttpGet]

        public ActionResult ResumeTables()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(p => p.UserName == username).FirstOrDefault();
            int employeeid = db.EmployeeTB.Where(p => p.UserID == user.UserID).FirstOrDefault().EmployeeID;
            var resumes = db.ResumesHasSentForEachKarFarm(employeeid).ToList();
            List<ResumeTables> ResumeTables = new List<ResumeTables>();
            int i = 1;
            foreach (var item in resumes)
            {
                var f = new ResumeTables();
                f.ResumeID = item.ResumeID;
                f.count = i++;
                f.DateSent = item.RequestDate;
                ResumeTables.Add(f);
            }
            return View(ResumeTables);
        }
        public ActionResult ShowResumeDetails(int id)
        {
            var res = db.ResumeTB.Find(id);
            return PartialView(res);
        }
    }
}