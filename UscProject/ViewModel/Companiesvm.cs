using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UscProject.Models;

namespace UscProject.ViewModel
{
    public class Companiesvm
    {
        public string img;
        public string name;
        public int id;
        public int FormsCount;
        public string address;
    }
    public class JobListsvm
    {
        public TheTotallFormsOfKarfarma_Result jobs;
        
        public string jobcat;
    
    }
}