using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Notes.Controllers
{
    public class OUList
    {
        public int Id { get; set; }
        [Display(Name = "Organizational Unit Name")]
        public string OUName { get; set; }
    }
}
