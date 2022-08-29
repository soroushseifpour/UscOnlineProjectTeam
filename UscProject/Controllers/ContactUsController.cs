using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;
using UscProject.ViewModel;

namespace UscProject.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public JsonResult SendComment(ContactTB contact)
        {
            contact.ContactDate = DateTime.Now;
                db.Entry(contact).State = System.Data.Entity.EntityState.Added;
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