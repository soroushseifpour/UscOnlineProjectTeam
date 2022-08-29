using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UscProject.Models;
using UscProject.ViewModel;

namespace UscProject.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        OnlineProjectUSCEntities db = new OnlineProjectUSCEntities();
        [Route("RegisterKarjo")]
        public ActionResult RegisterKarjo() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RegisterKarjo")]
        public ActionResult RegisterKarjo(Accountvm accountvm)
        {
            if (ModelState.IsValid) 
            {
                if (!db.UserTB.Any(u => u.Email == accountvm.Email.Trim().ToLower() || u.UserName==accountvm.UserName))
                {
                    UserTB user = new UserTB() {
                        Email = accountvm.Email,
                        Password = accountvm.Password,
                        ActiveCode = Guid.NewGuid().ToString(),
                        ImageName = false,
                        PictureName = "UserProfile.jpg",
                        IsActive=true,
                        RoleID=3,
                        RegesterDate=DateTime.Today,
                        UserName=accountvm.UserName
                    };
                    db.UserTB.Add(user);
                    EmployerTB employer = new EmployerTB();
                    employer.UserID = user.UserID;
                    db.EmployerTB.Add(employer);
                    db.SaveChanges();
                }
                else 
                {
                    if (db.UserTB.Any(u => u.Email == accountvm.Email.Trim().ToLower()))
                    {
                        ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است!");
                    }
                    else 
                    {
                        ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است!");
                    }
                    
                }
            }
            return View(accountvm);
        }


        [Route("RegisterKarfarma")]
        public ActionResult RegisterKarfarma()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("RegisterKarfarma")]
        public ActionResult RegisterKarfarma(AccountKarfamavm accountvm)
        {
            if (ModelState.IsValid)
            {
                if (!db.UserTB.Any(u => u.Email == accountvm.Email.Trim().ToLower() || u.UserName == accountvm.UserName) || !db.EmployeeTB.Any(u=>u.CompanyName == accountvm.CompanyName))
                {
                    UserTB user = new UserTB()
                    {
                        Email = accountvm.Email,
                        Password = accountvm.Password,
                        ActiveCode = Guid.NewGuid().ToString(),
                        ImageName = false,
                        PictureName = "UserProfile.jpg",
                        IsActive = true,
                        RoleID = 2,
                        RegesterDate = DateTime.Today,
                        UserName = accountvm.UserName
                    };
                    db.UserTB.Add(user);
                    EmployeeTB employeeTB = new EmployeeTB() {
                        CompanyName=accountvm.CompanyName,
                        PhoneNumber = Int64.Parse(accountvm.PhoneNumber),
                        Site=accountvm.Site,
                        UserID=user.UserID
                    };
                    db.EmployeeTB.Add(employeeTB);
                    db.SaveChanges();
                }
                else
                {
                    if (db.UserTB.Any(u => u.Email == accountvm.Email.Trim().ToLower()))
                    {
                        ModelState.AddModelError("Email", "ایمیل وارد شده تکراری است!");
                    }
                    else if (db.UserTB.Any(u => u.UserName == accountvm.UserName))
                    {
                        ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است!");
                    }
                    else if (db.EmployeeTB.Any(u => u.CompanyName == accountvm.CompanyName))
                    {
                        ModelState.AddModelError("CompanyName", "نام شرکت وارد شده تکراری است!");
                    }

                }
            }
            return View(accountvm);
        }



        [Route("Login")]
        public ActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(Loginvm loginvm,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                var user = db.UserTB.SingleOrDefault(e => e.Email == loginvm.Email && e.Password == loginvm.Password);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, loginvm.RememberMe);
                        TempData["name"] = user.RoleID;
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                    }
                }
                else 
                {
                 ModelState.AddModelError("Email", "کاربری یافت نشد");
                }
            }
            return View(loginvm);
        }
        public ActionResult RedirectToDashBoard(string name) 
        {
            var user = db.UserTB.Where(p => p.UserName == name).SingleOrDefault();
            if (user.RoleID == 2)
            {
                return RedirectToAction("Index", "KarFarma/Panel");
            }
            else if (user.RoleID == 3) 
            {
                return RedirectToAction("Index", "KarJo/Panel");
            }
            return RedirectToAction("Index", "Admin/Panel");
        }
        [Route("LogOut")]
        public ActionResult LogOut() 
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}