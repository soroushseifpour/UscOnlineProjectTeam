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
    
    public partial class ResumeTB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResumeTB()
        {
            this.EmployerTB = new HashSet<EmployerTB>();
            this.ResumeEmployeeTB = new HashSet<ResumeEmployeeTB>();
        }
    
        public int ResumeID { get; set; }
        public string FirstName { get; set; }
        public string LatsName { get; set; }
        public int Age { get; set; }
        public System.DateTime RequestDate { get; set; }
        public string EduDegree { get; set; }
        public string AbilityDescription { get; set; }
        public bool EnglishAbility { get; set; }
        public string ExtraAbility { get; set; }
        public string University { get; set; }
        public string Experience { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployerTB> EmployerTB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResumeEmployeeTB> ResumeEmployeeTB { get; set; }
    }
}