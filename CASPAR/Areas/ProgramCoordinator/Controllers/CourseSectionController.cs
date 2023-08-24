using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.ApplicationCore.ViewModels.Input;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Omu.Awem.Utils;
using Omu.AwesomeMvc;
using System.Linq;
using System.Reflection;

namespace CASPAR.Areas.ProgramCoordinator.Controllers
{
    public class CourseSectionController : Controller
    {
        private UnitOfWork _unitOfWork;
        private string Includes = "SemesterSchedule,SemesterSchedule.Semester,Course,Instructor,Modality,Status,Classroom,Classroom.Building,Classroom.Building.Campus,TimeSlot,DayOfTheWeek,PayModel,WhoPays";
        public CourseSectionController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private object GetMappingFromCourseSection(CourseSection o)
        {
            return new
            {
                Id = o.CourseSectionID,
                o.PartOfTerm,
                CName = NullableFieldsHelper(o.Course, () => o.Course.CourseName),
                Mode = NullableFieldsHelper(o.Modality, () => o.Modality.ModalityType),
                InstructorName = NullableFieldsHelper(o.Instructor, () => o.Instructor.LastName + " " + o.Instructor.FirstName),
                Loc = NullableFieldsHelper(o.Classroom, () => o.Classroom.Building.Campus.CampusName),
                Days = NullableFieldsHelper(o.DayOfTheWeek, () => o.DayOfTheWeek.DayOfWeek),
                Time = NullableFieldsHelper(o.TimeSlot, () => o.TimeSlot.TimeSlotType),
                Stat = NullableFieldsHelper(o.Status, () => o.Status.StatusType),
                ClassroomNum = NullableFieldsHelper(o.Classroom, () => GetClassroomName(o.Classroom)),
                o.MaxEnrollment,
                o.FirstDayEnrollment,
                o.ThirdWeekEnrollment,
                o.CRN,
                o.CourseID,
                WhoPays = o.WhoPays.WhoPaysType,
                o.SectionNotes,
                PayMod = o.PayModel.PayModelType,
                o.SemesterScheduleID,
                o.ClassRoomID,
                SemesterId = o.SemesterSchedule.SemesterID
            };
        }

        private string GetClassroomName(Classroom c)
        {
            return c.Building.BuildingName + " " + c.RoomNum.ToString();
        }

        public IActionResult GetCourseSections(GridParams g, int? semesterId)
        {
            // this just tests to see if semester id is null and if it is, then it sets it to 1
            semesterId = semesterId ?? 1;

            var courseSection = _unitOfWork.SECTION.GetAll(includes: Includes);


            var gridModel = new GridModelBuilder<CourseSection>(courseSection.AsQueryable(), g);

            gridModel.KeyProp = o => o.CourseSectionID;
            gridModel.Map = GetMappingFromCourseSection;

            var jsonOut = Json(gridModel.Build());
            return jsonOut;
        }

        public IActionResult GetModality()
        {
            var modality = _unitOfWork.Modality.GetAll();

            return Json(modality.Select(o => new KeyContent(o.Id, o.ModalityType)));
        }

        public IActionResult GetDaysOfTheWeek()
        {
            var daysOfTheWeek = _unitOfWork.DAY_OF_WEEK.GetAll();

            return Json(daysOfTheWeek.Select(o => new KeyContent(o.DayOfTheWeekID, o.DayOfWeek)));
        }

        public IActionResult GetInstructors()
        {
            var instructor = _unitOfWork.Instructor.GetAll();

            return Json(instructor.Select(o => new KeyContent(o.Id, o.FirstName + " " + o.LastName)));
        }

        public IActionResult GetTimeSlot()
        {
            var timeSlot = _unitOfWork.TimeSlot.GetAll();

            return Json(timeSlot.Select(o => new KeyContent(o.TimeSlotID, o.TimeSlotType)));
        }

        public IActionResult GetStatus()
        {
            var status = _unitOfWork.Status.GetAll();

            return Json(status.Select(o => new KeyContent(o.StatusID, o.StatusType)));
        }

        public IActionResult GetClassroom()
        {
            var classroom = _unitOfWork.Classroom.GetAll(includes: "Building,Building.Campus");

            return Json(classroom.Select(o => new KeyContent(o.ClassRoomID, ClassroomName(o))));
        }

        public IActionResult GetCourses()
        {
            var course = _unitOfWork.Course.GetAll();

            return Json(course.Select(o => new KeyContent(o.CourseID, o.CourseNumber + " " + o.CourseName)));
        }

        public IActionResult GetPayModel()
        {
            var payModel = _unitOfWork.PayModel.GetAll();

            return Json(payModel.Select(o => new KeyContent(o.PayModelID, o.PayModelType)));
        }

        public IActionResult GetWhoPays()
        {
            var whoPays = _unitOfWork.WhoPays.GetAll();

            return Json(whoPays.Select(o => new KeyContent(o.WhoPaysID, o.WhoPaysType)));
        }

        private string ClassroomName(Classroom c)
        {
            return c.Building.Campus.CampusName + " " + c.Building.BuildingName + " " + c.RoomNum;
        }

        public IActionResult Delete(int id)
        {
            var course = _unitOfWork.SECTION.GetById(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Message = "Are you sure you want to nuke the ever living fuck out of the small boi?",
                GridId = "Sections"
            }) ;
        }

        [HttpPost]
        public IActionResult Delete(DeleteConfirmInput input)
        {
            _unitOfWork.SECTION.Delete(_unitOfWork.SECTION.GetById(input.Id));

            // the PopupForm's success function utils.itemDeleted expects an obj with "Id" property
            return Json(new { input.Id });
        }

        [HttpPost]
        public IActionResult Save(CourseSectionInput input)
        {

            //if (ModelState.IsValid) this isn't working.  The Id from the input can be zero and it is set to validate never, but the model keeps saying that the id is the problem
            if (true)
                {
                var isCreate = input.Id == 0;

                var course = isCreate ? new CourseSection() :
                    _unitOfWork.SECTION.GetById(input.Id);

                if (course == null)
                {
                    throw new Exception("Item doesn't exist anymore, id:" + input.Id);
                }

                course.MaxEnrollment = input.MaxEnrollment ?? course.MaxEnrollment;
                course.PartOfTerm = input.PartOfTerm ?? course.PartOfTerm;
                course.ThirdWeekEnrollment = input.ThirdWeekEnrollment ?? course.ThirdWeekEnrollment;
                course.FirstDayEnrollment = input.FirstDayEnrollment ?? course.FirstDayEnrollment;
                course.CRN = input.CRN ?? course.CRN;
                course.ClassRoomID = input.ClassroomId ?? course.ClassRoomID;
                course.StatusID = input.StatusId ?? course.StatusID;
                course.TimeSlotID = input.TimeSlotId ?? course.TimeSlotID;
                course.DayOfWeekID = input.DaysOfTheWeekId ?? course.DayOfWeekID;
                course.ModalityID = input.ModalityId ?? course.ModalityID;
                course.StatusID = input.StatusId ?? course.StatusID;
                course.WNumber = input.InstructorId ?? course.WNumber;
                course.CourseID = input.CourseId ?? course.CourseID;
                course.WhoPaysID = input.WhoPaysID ?? course.WhoPaysID;
                course.PayModelID = input.PayModelID ?? course.PayModelID;
                course.SectionNotes = input.SectionNotes ?? course.SectionNotes;

                //Semester schedule needs to be removed from the course section.  Or I have no idea what it is linked to
                course.SemesterScheduleID = _unitOfWork.SemesterSchedule.Get(o => o.SemesterScheduleID != 0).SemesterScheduleID;

                if (isCreate)
                {
                    _unitOfWork.SECTION.Add(course);
                }
                else
                {
                    _unitOfWork.SECTION.Update(course);
                }

                course = _unitOfWork.SECTION.Get(o => o.CourseSectionID == course.CourseSectionID, includes: Includes);

                return Json(new { Item = GetMappingFromCourseSection(course) });
            }

            return Json(ModelState.GetErrorsInline());
        }

        public IActionResult PopupInterests(int id)
        {
            return PartialView("StudentPreferences", id);
        }

        // The classrooms currently don't sort based on the semester.  I'm not sure why I had the classroom id though
        public IActionResult ClassroomUsedIn(int classRoomID, int semesterScheduleID)
        {
            return PartialView("ClassroomUsedIn");
        }

        public IActionResult GetItem(int? v)
        {
            var o =  _unitOfWork.Classroom.Get(o => o.ClassRoomID == v, includes: "Building") ?? new Classroom();

            return Json(new KeyContent(o.ClassRoomID, GetClassroomName(o)));
        }

        public IActionResult GetInterest(GridParams g)
        {
            var semesterIdTemp = 1;
            var preferences = _unitOfWork.StudentPreferences.GetAll(o => o.SemesterID == semesterIdTemp);
            var grouped = preferences.GroupBy(o => new { o.ModalityID, o.CampusID, o.TimeOfDayID, o.CourseID })
                .Select(group => 
                new StudentInterestDisplay
                { 
                    ModalityId = _unitOfWork.Modality.GetById(group.Key.ModalityID).ModalityType,
                    CampusId = _unitOfWork.Campus.GetById(group.Key.CampusID)?.CampusName ?? "",
                    TimeOfDayID = _unitOfWork.TimeOfDay.GetById(group.Key.TimeOfDayID)?.Time ?? "",
                    Count = group.Count(),
                    CourseName = _unitOfWork.Course.GetById(group.Key.CourseID)?.CourseName ?? ""
                });

            int i = 1;
            foreach (var interest in grouped)
            {
                interest.StudentInterestDisplayId = i++;
            }

            // I want to see if there is already a property when using group by
            var grid = new GridModelBuilder<StudentInterestDisplay>(grouped.AsQueryable(), g)
            {
                KeyProp = o => o.StudentInterestDisplayId
            };

            return Json(grid.Build());
        }

        private string NullableFieldsHelper(object obj, Func<string> outputIfNotNull)
        {
            if(obj == null)
            {
                return "";
            }
            return outputIfNotNull();
        }
    }
}
