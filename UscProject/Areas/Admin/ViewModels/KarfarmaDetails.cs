using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UscProject.Models;

namespace UscProject.Areas.Admin.ViewModels
{
    public class KarfarmaDetails
    {
        public List<TheTotallFormsOfKarfarma_Result> karfarma_Result;
        public EmployeeTB employee;
        public List<TheResumeForEmployees_Result> resumeForEmployees_Result;
        public bool PicStatus;
        public string UserName;
    }
}