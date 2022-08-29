using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UscProject.Models;

namespace UscProject.ViewModel
{
    public class Candidatesvm
    {
        public string ImageName;
        public string FirstName;
        public string LastName;
        public string Ability;
        public int id;
    }
    public class candidatesdetailsvm 
    {
        public ResumeTB Resume;
        public string ImageName;
        public string Email;
    }
}