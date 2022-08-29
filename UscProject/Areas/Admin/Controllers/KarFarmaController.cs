using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.Admin.ViewModels;
using UscProject.Models;

namespace UscProject.Areas.Admin.Controllers
{
    public class KarFarmaController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: Admin/KarFarma
        public ActionResult KarfarmaTables()
        {
            List<KarfarmaInformations> karfarmas = new List<KarfarmaInformations>();

            var model = db.EmployeeTB.ToList();
            foreach (var g in model)
            {
                KarfarmaInformations temp = new KarfarmaInformations();
                temp.Employee = g;
                temp.company = db.EmployeeTB.Where(p => p.UserID == g.UserID).Select(p => p.CompanyName).FirstOrDefault();
                temp.DateOfRegister = db.UserTB.Where(p => p.UserID == g.UserID).First().RegesterDate;
                temp.email = db.UserTB.Where(p => p.UserID == g.UserID).First().Email;
                temp.name = db.UserTB.Where(p => p.UserID == g.UserID).First().UserName;
                karfarmas.Add(temp);
            }
            return View(karfarmas);
        }
        public ActionResult ShowKarfarmaDetails(int id)
        {
            var Employee = db.EmployeeTB.Where(p => p.EmployeeID == id).First();
            var user = db.UserTB.Where(p => p.UserID == Employee.UserID).First();
            var Forms = db.TheTotallFormsOfKarfarma(id);
            var resumes = db.TheResumeForEmployees(id);
            KarfarmaDetails details = new KarfarmaDetails();
            details.employee = Employee;
            details.karfarma_Result = Forms.ToList();
            details.resumeForEmployees_Result = resumes.ToList();
            details.UserName = user.PictureName;
            if (user.ImageName)
            {
                details.PicStatus = true;
            }
            else
            {
                details.PicStatus = false;
            }
            return View(details);
        }
        public ActionResult ShowFormsDetails(int id)
        {
            var forms = db.FormTB.Find(id);
            return PartialView(forms);
        }
    }
}