using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Family.Controllers
{
    public class PersonList
    {
        // Display only, for UI item-selection controls, does not need annotations
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public string FullName 
        { 
            get
            {
                return string.Format("{0}, {1}", FamilyName, GivenNames);
            }
        }
    }

    // Not used in this app
    /*
    public class PersonAddForm
    {
        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name = "Family name")]
        public string FamilyName { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name = "Given name(s)")]
        public string GivenNames { get; set; }

        [Required, StringLength(10, MinimumLength = 4)]
        [Display(Name = "Gender, male or female")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
    */

    public class PersonAdd
    {
        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name="Family name")]
        public string FamilyName { get; set; }

        [Required, StringLength(100, MinimumLength = 2)]
        [Display(Name="Given name(s)")]
        public string GivenNames { get; set; }

        [Required, StringLength(10, MinimumLength = 4)]
        [Display(Name="Gender, male or female")]
        public string Gender { get; set; }

        [Required]
        [Display(Name="Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }

    public class PersonAddFromCsv
    {
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class PersonBase : PersonAdd
    {
        [HiddenInput]
        public int Id { get; set; }
    }

    public class PersonBaseEditFamilyForm : PersonBase
    {
        public SelectList Father { get; set; }
        public SelectList Mother { get; set; }

        // Notice the multi-select list
        public MultiSelectList Children { get; set; }
    }

    public class PersonBaseEditFamily : PersonBase
    {
        public PersonBaseEditFamily()
        {
            this.ChildrenIds=new List<int>();
        }

        public int? FatherId { get; set; }
        public int? MotherId { get; set; }

        public IEnumerable<int> ChildrenIds { get; set; }
    }

    public class PersonBaseWithRelations : PersonBase
    {
        public PersonBaseWithRelations()
        {
            this.Children = new List<PersonBase>();
        }

        public PersonBase Father { get; set; }
        public PersonBase Mother { get; set; }
        public IEnumerable<PersonBase> Children { get; set; }
    }
}
