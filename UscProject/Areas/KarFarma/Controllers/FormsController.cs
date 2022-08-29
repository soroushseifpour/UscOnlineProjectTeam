using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.KarFarma.ViewModel;
using UscProject.Models;

namespace UscProject.Areas.KarFarma.Controllers
{
    public class FormsController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: KarFarma/Forms
        public ActionResult AddForms()
        {
            var model = db.JobCategoryTB.ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult AddForms(FormTB form)
        {
            //....ایا تعدا  فرم های نوشته شده از حد مجاز بیشتر شده یا نه
            //...ایا مهلت سفارش گذشته یا نه
            //...مورد اول
            var username = User.Identity.Name;
            var user = db.UserTB.Where(p => p.UserName == username).FirstOrDefault();
            int employeeid = db.EmployeeTB.Where(p => p.UserID == user.UserID).FirstOrDefault().EmployeeID;
            var thelastorder = db.OrderDetailTB.Where(e => e.EmployeeID == employeeid).OrderByDescending(e => e.BuyDate).First();
            var TheTotallpermisson = thelastorder.BoxCategory.CountPermisson;
            var today = DateTime.Today;
            var TheTotallForms = db.TheTotallFormsAfterTheLastOrderForEmployee_finallversion(employeeid, today).Count();
            var thefinaldate = thelastorder.BuyDate.Value.AddDays(thelastorder.BoxCategory.DatePermission);
            if (TheTotallForms == TheTotallpermisson || today > thefinaldate)
            {

                return Json(new JsonData()
                {
                    Status = false
                });
            }
            else
            {
                form.OrderDetailID = thelastorder.OrderDetailID;
                form.RequestDtae = today;
                db.Entry(form).State = EntityState.Added;
                var formdetail = new FormDetailTB();
                formdetail.EmoloyeeID = employeeid;
                formdetail.FormID = form.FormID;
                db.FormDetailTB.Add(formdetail);
                db.SaveChanges();
                return Json(new JsonData()
                {
                    Status = true
                });

            }
        }
        public ActionResult FormTables()
        {

            List<FormTable> form = new List<FormTable>();
            var username = User.Identity.Name;
            var user = db.UserTB.Where(p => p.UserName == username).FirstOrDefault();
            int employeeid = db.EmployeeTB.Where(p => p.UserID == user.UserID).FirstOrDefault().EmployeeID;
            var count = db.TheTotallFormsOfKarfarma(employeeid).ToList();
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
        public ActionResult DeleteForm()
        {
            if (TempData.ContainsKey("FormID"))
            {
                int id = Int32.Parse(TempData["FormID"].ToString());
                var form = db.FormTB.Find(id);
                db.Entry(form).State = EntityState.Deleted;
                var formdetail = db.FormDetailTB.Where(o => o.FormID == id).First();
                db.FormDetailTB.Remove(formdetail);
                var resumeemployee = db.ResumeEmployeeTB.Where(o => o.FormID == id).ToList();
                foreach (var item in resumeemployee)
                {
                    db.ResumeEmployeeTB.Remove(item);
                }
                db.SaveChanges();
            }

            return RedirectToAction("FormTables");
        }
        public ActionResult ShowFormsDetails(int id)
        {
            var forms = db.FormTB.Find(id);
            return PartialView(forms);
        }
        public ActionResult EditForms(int id)
        {
            var model = new EditForms();
            var forms = db.FormTB.Find(id);
            model.form = forms;
            model.jobs = db.JobCategoryTB.ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult EditForms(FormTB form)
        {
            form.UpdateDate = DateTime.Now;
            db.Entry(form).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new JsonData()
            {
                Status = true
            });
        }
        public class JsonData
        {
            public bool Status { get; set; }
            public string Message { get; set; }

        }
    }
}