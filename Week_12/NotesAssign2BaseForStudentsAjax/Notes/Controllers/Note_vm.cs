using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.Controllers
{
    public class NoteAddForm
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        public string NoteText { get; set; }

        [HiddenInput]
        public int EmployeeId { get; set; }
    }

    public class NoteAdd
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        public string NoteText { get; set; }

        [HiddenInput]
        public int EmployeeId { get; set; }
    }

    public class NoteBase : NoteAdd
    {
        [HiddenInput]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
