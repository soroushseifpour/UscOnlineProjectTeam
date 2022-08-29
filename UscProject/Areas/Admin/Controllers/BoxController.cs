using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;

namespace UscProject.Areas.Admin.Controllers
{
    public class BoxController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: Admin/Box
        
        [HttpGet]
        public ActionResult Boxes()
        {
            var box = db.BoxCategory.ToList();
            return View(box);

        }
        public ActionResult ShowBox(int id)
        {
            var box = db.BoxCategory.Find(id);
            return PartialView(box);
        }
        public ActionResult EditBox(int id)
        {

            var box = db.BoxCategory.Find(id);

            return View(box);
        }

        [HttpPost]
        public JsonResult EditBoxDetails(BoxCategory boxCategory)
        {

            db.Entry(boxCategory).State = EntityState.Modified;

            db.SaveChanges();
            return Json(new JsonData()
            {
                Status = true
            });
        }
        public ActionResult AddNewBox()
        {

            return View();

        }

        [HttpPost]
        public JsonResult AddNewBox(BoxCategory box)
        {

            db.Entry(box).State = EntityState.Added;
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