using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.Admin.ViewModels;
using UscProject.Models;

namespace UscProject.Areas.Admin.Controllers
{
    public class KarJoController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: Admin/KarJo

        public ActionResult KarjoTables()
        {
            List<KarjoInformation> karjos = new List<KarjoInformation>();
            var karjo = db.EmployerTB.ToList();
            foreach (var item in karjo)
            {
                var temp = new KarjoInformation();
                int? id = db.EmployerTB.Where(p => p.EmployerID == item.EmployerID).First().ResumeID;
                temp.UserName = db.UserTB.Where(p => p.UserID == item.UserID).First().UserName;
                temp.Email = db.UserTB.Where(p => p.UserID == item.UserID).First().Email;
                int userid = db.EmployerTB.Where(p => p.EmployerID == item.EmployerID).First().UserID;
                temp.dateofregister = db.UserTB.Where(p => p.UserID == userid).First().RegesterDate;
                temp.id = id;
                karjos.Add(temp);
            }
            return View(karjos);
        }
        public ActionResult ShowKarjoInformation(int id)
        {
            var model = new ShowKarjoInformation();
            model.theForms = db.TheFormsOFEachResumeFinal(id).ToList();
            int userid = db.EmployerTB.Where(p => p.ResumeID == id).FirstOrDefault().UserID;
            var user = db.UserTB.Find(userid);
            model.Email = user.Email;
            model.UserName = user.UserName;
            if (user.ImageName)
            {
                model.ImageName = user.PictureName;
                model.PicStatus = true;
            }
            else
            {
                model.ImageName = null;
                model.PicStatus = false;
            }
            return View(model);
        }
        public ActionResult ShowFormsDetails(int id)
        {
            var forms = db.FormTB.Find(id);
            return PartialView(forms);
        }

    }
}