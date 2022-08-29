using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UscProject.Models;

namespace UscProject.Areas.Admin.ViewModels
{
    public class ShowKarjoInformation
    {
        public string UserName;
        public string Email;
        public string ImageName;
        public List<TheFormsOFEachResumeFinal_Result> theForms;
        public bool PicStatus;
    }
}