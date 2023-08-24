using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.ViewModels
{
    public class SemesterScheduleViewModel
    {
        public string ScheduleName { get; set; }
        public string Semester { get; set; }
        public string TemplateName { get; set; }
        public int CourseQuantity { get; set; }
        public DateTime OpenEnrollmentDate { get; set; }
        public DateTime CloseEnrollmentDate { get; set; }
        public string Status { get; set; }
        public string Course { get; set; }
    }
}
