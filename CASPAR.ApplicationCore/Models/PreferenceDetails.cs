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
	public class PreferenceDetails
	{
		[Key]
		public int PreferenceDetailsID { get; set; }


		[Required]
		public int ModalityID { get; set; }

		[ForeignKey("ModalityID")]
		[ValidateNever]
		public Modality Modality { get; set; }


		public int? DayOfWeekID { get; set; }

		[ForeignKey("DayOfWeekID")]
		[ValidateNever]
		public DayOfTheWeek? DayOfWeek { get; set; }


		public int? TimeOfDayID { get; set; }

		[ForeignKey("TimeOfDayID")]
		[ValidateNever]
		public TimeOfDay? TimeOfDay { get; set; }


		public int? CampusID { get; set; }

		[ForeignKey("CampusID")]
		[ValidateNever]
		public Campus? Campus { get; set; }

		[Required]
		public String WNumber { get; set; }

		[Required]
		[ForeignKey("WNumber")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }


		[Required]
		public int CourseID { get; set; }

		[Required]
		[ForeignKey("CourseID")]
		[ValidateNever]
		public Course Course { get; set; }


		[Required]
		public int SemesterID { get; set; }
		[ForeignKey("SemesterID")]
		[ValidateNever]
		public Semester Semester { get; set; }

		/// <summary>
		/// Return a asp awesome safe version of DayOfWeek.DayOfWeek
		/// </summary>
		[NotMapped]
		public string SafeDayOfWeekDay { get { return DayOfWeek != null ? DayOfWeek.DayOfWeek : "N/A"; } }

		/// <summary>
		/// Return a asp awesome safe version of TimeOfDay.Time
		/// </summary>
		[NotMapped]
		public string SafeTimeOfDayTime { get { return TimeOfDay != null ? TimeOfDay.Time : "N/A"; } }

		/// <summary>
		/// Return a asp awesome safe version of Campus.CampusName
		/// </summary>
		[NotMapped]
		public string SafeCampusName { get { return Campus != null ? Campus.CampusName : "N/A"; } }


		// // Might not work

		//public String? InstructorID { get; set; }

		//[Required]
		//[ForeignKey("InstructorID")]
		//[ValidateNever]
		//public Instructor? Instructor { get; set; }


		//public String? StudentID { get; set; }

		//[Required]
		//[ForeignKey("InstructorID")]
		//[ValidateNever]
		//public Student? Student { get; set; }
	}
}
