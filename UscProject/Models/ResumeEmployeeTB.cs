//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UscProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResumeEmployeeTB
    {
        public Nullable<int> ResumeID { get; set; }
        public int FormID { get; set; }
        public System.DateTime Date { get; set; }
        public int ResumeEmployeeID { get; set; }
    
        public virtual ResumeTB ResumeTB { get; set; }
        public virtual FormTB FormTB { get; set; }
    }
}
