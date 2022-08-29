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
    public class CandidateController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: Candidates
        [Route("Candidates")]
        public ViewResult Candidates(int? page)
        {
            var resume = db.ResumeTB.ToList();
            List<Candidatesvm> candidatesvms = new List<Candidatesvm>();
            foreach (var item in resume)
            {
                Candidatesvm candidatesvm = new Candidatesvm();
                var userid = db.EmployerTB.Where(p => p.ResumeID == item.ResumeID).FirstOrDefault();
                bool img = db.UserTB.Where(p => p.UserID == userid.UserID).First().ImageName;
                var user = db.UserTB.Find(userid.UserID);
                candidatesvm.FirstName = item.FirstName;
                candidatesvm.LastName = item.LatsName;
                candidatesvm.Ability = item.AbilityDescription;
                candidatesvm.id = item.ResumeID;
                if (img == false)
                {
                    candidatesvm.ImageName = Url.Content("/Files/UserPictures/Default/UserProfile.jpg");
                }
                else
                {
                    candidatesvm.ImageName = Url.Content("/Files/UserPictures/Custom/KarJo/" + user.PictureName);
                }
                candidatesvms.Add(candidatesvm);
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(candidatesvms.ToPagedList(pageNumber, pageSize));

        }
        [Route("Candidates/{id}")]
        public ActionResult CandidatesDetails(int id)
        {
            var resume = db.ResumeTB.Find(id);
            int uid = db.EmployerTB.Where(e => e.ResumeID == id).FirstOrDefault().UserID;
            var user = db.UserTB.Find(uid);
            var img = db.UserTB.Where(e => e.UserID == uid).FirstOrDefault().ImageName;
            if (img == true)
            {
                ViewBag.pic = Url.Content("/Files/UserPictures/Custom/KarJo/" + user.PictureName);
            }
            else
            {
                ViewBag.pic = Url.Content("/Files/UserPictures/Default/UserProfile.jpg");
            }
            ViewBag.Email = user.Email;
            return View(resume);
        }
    }
}