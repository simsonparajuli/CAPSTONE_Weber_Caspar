using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class SemesterSchedule
	{
		[Key]
		public int SemesterScheduleID { get; set; }

		[Required]
		public int SemesterTemplateID { get; set; }

		[ValidateNever]
		[ForeignKey("SemesterTemplateID")]
		public SemesterTemplate SemesterTemplate { get; set; }

        [Required]
		public int StatusID { get; set; }

		[ValidateNever]
        [ForeignKey("StatusID")]
		public Status Status { get; set; }

        [Required]
		[Display (Name = "Schedule Name")]
		public string ScheduleName { get; set; }

		[Required]
        [Display(Name = "Open Enrollment Date")]
        public DateTime OpenEnrollmentDate { get; set; }

		[Required]
        [Display(Name = "Close Enrollment Date")]
        public DateTime CloseEnrollmentDate { get; set; }

		[Required]
		public int SemesterID { get; set; }

		[ValidateNever]
        [ForeignKey("SemesterID")]
		public Semester Semester { get; set; }
    }
}
