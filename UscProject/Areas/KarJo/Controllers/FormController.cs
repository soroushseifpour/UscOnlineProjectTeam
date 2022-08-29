using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.KarJo.ViewModel;
using UscProject.Models;

namespace UscProject.Areas.KarJo.Controllers
{
    public class FormController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: KarJo/Form
        public ActionResult FormTables()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            int userid = user.UserID;
            var employer = db.EmployerTB.Where(e => e.UserID == userid).First();
            int employerid = employer.EmployerID;
            int? resumid = db.EmployerTB.Where(e => e.UserID == user.UserID).First().ResumeID;
            var count = db.TheFormsOFEachResume(resumid).ToList();

            List<FormTable> form = new List<FormTable>();
            int i = 1;
            foreach (var item in count)
            {
                var f = new FormTable();
                f.formid = item.FormID;
                f.count = i++;
                f.DateOfMake = item.RequestDtae;
                form.Add(f);
            }
            return View(form);
        }
        public ActionResult ShowFormsDetails(int id)
        {
            var form = db.FormTB.Find(id);
            return PartialView(form);
        }
        public void DeleteForm(int id)
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
            int userid = user.UserID;
            var employer = db.EmployerTB.Where(e => e.UserID == userid).First();
            int employerid = employer.EmployerID;
            int? resumid = db.EmployerTB.Where(e => e.UserID == user.UserID).First().ResumeID;
            var resmeemployee = db.ResumeEmployeeTB.Where(p => p.FormID == id && p.ResumeID == resumid).First();
            db.ResumeEmployeeTB.Remove(resmeemployee);
            db.SaveChanges();
        }
    }
}