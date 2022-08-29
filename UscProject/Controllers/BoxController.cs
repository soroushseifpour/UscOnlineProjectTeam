using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Models;

namespace UscProject.Controllers
{
    public class BoxController : Controller
    {
        // GET: Box
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();


        [Route("Boxes")]
        public ActionResult Index()
        {
            var box = db.BoxCategory.ToList();
            //....چک کنیم چه کسی لاگین هست
            if (User.Identity.IsAuthenticated)
            {

                var username = User.Identity.Name;
                var user = db.UserTB.Where(u => u.UserName == username).SingleOrDefault();
                int RoleId = user.RoleID;
                if (RoleId == 1)
                {
                    ViewBag.status = false;
                    ViewBag.message = "شما ادمین هستید!";
                }
                else if (RoleId == 2)
                {
                    //...وضعیت اخرین بسته را باید چک کنیم
                    int employeeid = db.EmployeeTB.Where(u => u.UserID == user.UserID).SingleOrDefault().EmployeeID;
                    var the_last_order = db.OrderDetailTB.Where(p => p.EmployeeID == employeeid).OrderByDescending(u => u.BuyDate).First();
                    var final_date_permission = the_last_order.BuyDate.Value.AddDays(the_last_order.BoxCategory.DatePermission);
                    var the_totall_forms_of_last_order = db.TheTotallFormsAfterTheLastOrderForEmployee_finallversion(employeeid, DateTime.Today).Count();
                    var the_totall_forms_permission = the_last_order.BoxCategory.CountPermisson;
                    if (final_date_permission < DateTime.Today)
                    {
                        if (the_totall_forms_of_last_order < the_totall_forms_permission)
                        {
                            ViewBag.status = false;
                            ViewBag.message = "شما هنوز بسته قبلی را میتوانید استفاده کنید!";
                        }
                        else
                        {
                            ViewBag.status = true;
                            ViewBag.message = "خرید کنید";
                        }

                    }
                    else
                    {
                        ViewBag.status = true;
                        ViewBag.message = "خرید کنید";
                    }
                }
                else if (RoleId == 3)
                {
                    ViewBag.status = false;
                    ViewBag.message = "شما کارجو هستید و دسترسی ندارید";
                }
            }
            else 
            {
                ViewBag.status = false;
                ViewBag.message = "به حساب کاربری خود وارد شوید!";
            }
            return View(box);
        }
       
    }
}