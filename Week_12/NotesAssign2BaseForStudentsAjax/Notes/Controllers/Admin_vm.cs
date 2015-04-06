using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.Controllers
{
    public class ApplicationUserEditForm
    {
        [HiddenInput]
        public string Id { get; set; }

        [HiddenInput]
        public string UserName { get; set; }

        [Display(Name="User's full name")]
        public string FullName { get; set; }

        //public SelectList IsManager { get; set; }
    }

    public class ApplicationUserEdit
    {
        // Not editable, passed back by the HTML Form
        public string Id { get; set; }
        // Not editable, passed back by the HTML Form
        public string UserName { get; set; }
        
        //public string Manager { get; set; }
    }

    public class ApplicationUserBase
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim> Claims { get; set; }
        public string IsManager { get; set; }
    }
}
