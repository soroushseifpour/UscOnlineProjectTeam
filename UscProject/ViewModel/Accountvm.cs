using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UscProject.Models;

namespace UscProject.ViewModel
{
    public class Accountvm
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
    public class AccountKarfamavm
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }

        [Display(Name = "نام شرکت")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string CompanyName { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ادرس سایت شرکت")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Site { get; set; }

    }
    public class Loginvm 
    {

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    public class Commentvm 
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string CommentUserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string CommentEmail { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string CommentText { get; set; }
        public List<Categoryvm> categoryvms;
    }


}