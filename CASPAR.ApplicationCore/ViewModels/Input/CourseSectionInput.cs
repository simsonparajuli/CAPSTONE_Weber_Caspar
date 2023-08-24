using Microsoft.AspNetCore.Mvc;
using Omu.AwesomeMvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CASPAR.ApplicationCore.ViewModels.Input
{
    public class CourseSectionInput
    {
        [ValidateNever]
        public int Id { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetModality", Controller = "CourseSection")]
        [DisplayName("Modality")]
        public int? ModalityId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetCourses", Controller = "CourseSection")]
        [DisplayName("Course")]
        public int? CourseId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetInstructors", Controller = "CourseSection")]
        [DisplayName("Instructor Name")]
        public string? InstructorId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetDaysOfTheWeek", Controller = "CourseSection")]
        [DisplayName("Days")]
        public int? DaysOfTheWeekId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetTimeSlot", Controller = "CourseSection")]
        [DisplayName("Time Slot")]
        public int? TimeSlotId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetStatus", Controller = "CourseSection")]
        [DisplayName("Status")]
        public int? StatusId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetClassroom", Controller = "CourseSection")]
        [DisplayName("Classroom")]
        public int? ClassroomId { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetPayModel", Controller = "CourseSection")]
        [DisplayName("Pay Model")]
        public int? PayModelID { get; set; }

        [UIHint("DropdownList")]
        [AweUrl(Action = "GetWhoPays", Controller = "CourseSection")]
        [DisplayName("Who Pays")]
        public int? WhoPaysID { get; set; }

        [DisplayName("Max Enrollment")]
        public int? MaxEnrollment { get; set; }

        [DisplayName("First Day Enrollment")]
        public int? FirstDayEnrollment { get; set; }

        [DisplayName("Third Week Enrollment")]
        public int? ThirdWeekEnrollment { get; set; }

        [DisplayName("CRN")]
        public int? CRN { get; set; }

        [DisplayName("Section Notes")]
        public string? SectionNotes { get; set; }

        [DisplayName("Part of Term")]
        public string? PartOfTerm { get; set; }
    }
}
