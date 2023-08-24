using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models.ViewModels
{
    public class InstructorDetailsViewModel
    {
        public string WNumber { get; set; }
        public string ProgramPreference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SpringLoad { get; set; }
        public int? SummerLoad { get; set; }
        public int? FallLoad { get; set; }
        public string Roles { get; set; }
        public string InstructorNotes { get; set; }
    }
}
