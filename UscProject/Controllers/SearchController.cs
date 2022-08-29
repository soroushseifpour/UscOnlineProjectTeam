using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;

namespace UscProject.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        public ActionResult Index()
        {
            var cat = db.JobCategoryTB.ToList();
            return View(cat);
        }
        public ActionResult Result(string JobDescRiption,string Location,int? CategorySelected) 
        {
            List<FormTB> forms = new List<FormTB>();

            if (JobDescRiption != "")
            {
                forms.AddRange(db.FormTB.Where(f => f.FormText.Contains(JobDescRiption) || f.JobDescRiption.Contains(JobDescRiption)).ToList());
            }
            if (Location != "")
            {
                forms.AddRange(db.FormTB.Where(f => f.City == Location));
            }
            if (CategorySelected.ToString() != null)
            {
                
                forms.AddRange(db.FormTB.Where(f => f.JobCategoryTB.JobID==CategorySelected));
            }
            forms.Distinct();

            return PartialView(forms);
        }
    }
}