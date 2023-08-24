using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace CASPAR.ApplicationCore.Models
{
	public class CourseSection
	{
		[Key]
		public int CourseSectionID { get; set; }

		[Required]
		[Display(Name = "Semester Schedule")]
		public int SemesterScheduleID { get; set; }

		[ForeignKey("SemesterScheduleID")]
		[ValidateNever]
		public SemesterSchedule SemesterSchedule { get; set; }

		//[Required]
		[Display(Name = "Course")]
        public int? CourseID { get; set; }

		[ForeignKey("CourseID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
        public Course Course { get; set; }

        //[Required]
        [Display(Name = "Instructor")]
        public string? WNumber { get; set; }

		[ForeignKey("WNumber")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
		public Instructor Instructor { get; set; }

        //[Required]
        [Display(Name = "Modality")]
        public int? ModalityID { get; set; }

		[ForeignKey("ModalityID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
		public Modality Modality { get; set; }

        //[Required]
        [Display(Name = "Status")]
		public int? StatusID { get; set; }

        [ForeignKey("StatusID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
		public Status Status { get; set; }

        //[Required]
        [Display(Name = "Classroom")]
		public int? ClassRoomID { get; set; }

        [ForeignKey("ClassRoomID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
		public Classroom Classroom { get; set; }

        //[Required]
        [Display(Name = "Time Slot")]
		public int? TimeSlotID { get; set; }

        [ForeignKey("TimeSlotID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
		public TimeSlot TimeSlot { get; set; }

        [Required]
		[Display(Name = "Days of the week")]
		public int? DayOfWeekID { get; set; }

		[ForeignKey("DayOfWeekID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
        public DayOfTheWeek DayOfTheWeek { get; set; }

        [StringLength(30)]
		public string? PartOfTerm { get; set; }

		public int? FirstDayEnrollment { get; set; }

		public int? ThirdWeekEnrollment { get; set; }

		public int? CRN { get; set; }

		public int? MaxEnrollment { get; set; }

		[StringLength(400)]
		public string? SectionNotes { get; set; }

		
		public int? PayModelID { get; set; }
        [ForeignKey("PayModelID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
        public PayModel PayModel { get; set; }

        public int? WhoPaysID { get; set; }
        [ForeignKey("WhoPaysID")]
		[ValidateNever]
		[DeleteBehavior(DeleteBehavior.NoAction)]
        public WhoPays WhoPays { get; set; }
    }
}
