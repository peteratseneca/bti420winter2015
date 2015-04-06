using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
{
    public class Employee
    {
        public Employee()
        {
            this.Notes = new List<Note>();
            this.DirectReports = new List<Employee>();
            this.BirthDate = DateTime.Now.AddYears(-25);
            this.HireDate = DateTime.Now.AddYears(-5);
            this.OU = "(none)";
        }

        public int Id { get; set; }

        // Will be matched with the ClaimsIdentity.User 'Name' property
        // Therefore, it is a LOGICAL association
        // It is NOT an association that requires navigation properties
        public string IdentityUserId { get; set; }

        [Required, StringLength(100)]
        public string FamilyName { get; set; }

        [Required, StringLength(100)]
        public string GivenNames { get; set; }
        
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }

        [Required, StringLength(100)]
        public string OU { get; set; }

        // To-one, self-referencing association
        // Optional, because an Employee may - or may not - have a 'manager'
        // Requires TWO properties
        // A nullable int to hold the 'foreign key'
        // And (obviously) a property of the same type
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public byte[] Photo { get; set; }
        public string PhotoContentType { get; set; }

        // To-many, self-referencing association
        // Simple collection property of the same type
        public ICollection<Employee> DirectReports { get; set; }

        public ICollection<Note> Notes { get; set; }
    }

    public class Note
    {
        public Note()
        {
            this.Timestamp = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(10000)]
        public string NoteText { get; set; }

        [Required]
        public Employee Employee { get; set; }
    }

    // Lookup table 
    public class OU
    {
        public int Id { get; set; }
        public string OUName { get; set; }
    }

}
