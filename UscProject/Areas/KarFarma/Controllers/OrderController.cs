using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UscProject.Areas.KarFarma.ViewModel;
using UscProject.Models;

namespace UscProject.Areas.KarFarma.Controllers
{
    public class OrderController : Controller
    {
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        // GET: KarFarma/Order
        public ActionResult ShoppingList()
        {
            var username = User.Identity.Name;
            var user = db.UserTB.Where(p => p.UserName == username).FirstOrDefault();
            int employeeid = db.EmployeeTB.Where(p => p.UserID == user.UserID).FirstOrDefault().EmployeeID;
            List<ShoppingList> lists = new List<ShoppingList>();
            var shop = db.OrderDetailTB.Where(e => e.EmployeeID == employeeid).ToList();
            int i = 1;
            foreach (var item in shop)
            {
                var s = new ShoppingList();
                s.datebuy = item.BuyDate;
                s.OrderID = item.OrderDetailID;
                s.count = i++;
                s.Status = item.Status;
                lists.Add(s);
            }
            return View(lists);
        }
        public ActionResult OrderDetails(int id)
        {
            var order = db.OrderDetailTB.Find(id);
            var SelectedOrder = new OrderDetails();
            //.....فروشنده
            ViewBag.AdminName = "پروژه علم و فرهنگ";
            ViewBag.AdminPhoneNumber = "09128629008";
            //.....خریدار
            int employeeid = db.OrderDetailTB.Where(o => o.OrderDetailID == id).FirstOrDefault().EmployeeID;
            int userid = db.EmployeeTB.Where(p => p.EmployeeID == employeeid).First().UserID;
            SelectedOrder.OrderID = id;
            SelectedOrder.boxname = order.BoxCategory.BoxName;
            SelectedOrder.UserName = db.UserTB.Where(p => p.UserID == userid).First().UserName;
            SelectedOrder.ComapnyName = db.EmployeeTB.Where(p => p.EmployeeID == employeeid).First().CompanyName;
            SelectedOrder.Email = db.UserTB.Find(userid).Email;
            SelectedOrder.datebuy = order.BuyDate;
            SelectedOrder.UnitPrice = order.UnitPrice;
            SelectedOrder.Price = order.BoxCategory.Price;
            SelectedOrder.PhoneNumber = db.EmployeeTB.Where(p => p.EmployeeID == employeeid).First().PhoneNumber;
            SelectedOrder.webSite = db.EmployeeTB.Where(p => p.EmployeeID == employeeid).First().Site;
            SelectedOrder.address = db.EmployeeTB.Where(p => p.EmployeeID == employeeid).First().Adress;
            if (order.Status)
            {
                SelectedOrder.status = true;
            }
            else
            {
                SelectedOrder.status = false;
            }
            return View(SelectedOrder);
        }
    }
}