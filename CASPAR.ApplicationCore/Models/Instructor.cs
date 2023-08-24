using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
    public class Instructor : ApplicationUser
    {
        [DisplayName("Type")]
        public string? InstructorType { get; set; }

        [DisplayName("Notes")]
        public string? InstructorNotes { get; set; }
    }
}
