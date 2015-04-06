using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.Controllers
{
    public class EmployeeList
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }

        [Display(Name = "Full name")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", FamilyName, GivenNames);
            }
        }
    }

    // Used ONLY! by the load-from-CSV task
    public class EmployeeAdd
    {
        [Display(Name = "Family name")]
        public string FamilyName { get; set; }

        [Display(Name = "Given name(s)")]
        public string GivenNames { get; set; }

        [Display(Name = "Email address")]
        public string IdentityUserId { get; set; }

        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Hire date")]
        public DateTime HireDate { get; set; }
    }

    public class EmployeeBase : EmployeeAdd
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Organizational unit name")]
        public string OU { get; set; }

        [Display(Name = "Full name")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", this.FamilyName, this.GivenNames);
            }
        }
    }

    public class EmployeeBaseWithAssociatedData : EmployeeBase
    {
        public EmployeeBaseWithAssociatedData()
        {
            this.ManagerFullName = "(none)";
        }

        [Display(Name = "Manager's name")]
        public string ManagerFullName { get; set; }
        public EmployeeBase Manager { get; set; }

        [Display(Name = "Employees who report to me")]
        public IEnumerable<EmployeeBase> DirectReports { get; set; }
        public IEnumerable<NoteBase> Notes { get; set; }
    }

    public class EmployeeDirectReportsForm
    {
        public int Id { get; set; }
        public MultiSelectList Employees { get; set; }
    }

    public class EmployeeDirectReports
    {
        public int Id { get; set; }
        public ICollection<int> EmployeeIds { get; set; }
    }

}
