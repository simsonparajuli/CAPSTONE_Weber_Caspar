using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Omu.AwesomeMvc;
using CASPAR.Utility.Utils.Awesome;

namespace CASPAR.Areas.ProgramCoordinator.Controllers
{
    public class ClassroomController : Controller
    {
        private UnitOfWork _unitOfWork;

        public ClassroomController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetClassrooms(GridParams g)
        {
            var classrooms = _unitOfWork.Classroom.GetAll(includes: "Building,Building.Campus");


            var gridModel = new GridModelBuilder<Classroom>(classrooms.AsQueryable(), g);

            gridModel.KeyProp = o => o.ClassRoomID;
            gridModel.Map = o => new
            {
                o.ClassRoomID,
                o.RoomNum,
                Name = o.Building.BuildingName,
                o.Capacity,
                UsedIn = GetUsedClassroomsWithTimes(1, o.ClassRoomID)
            };

            var jsonOut = Json(gridModel.Build());
            return jsonOut;
        }

        public string GetUsedClassroomsWithTimes(int semesterScheduleId, int classroomId)
        {
            var courseSection = _unitOfWork.SECTION.
                GetAll(o => o.SemesterScheduleID == semesterScheduleId && o.ClassRoomID == classroomId, includes: "Course,TimeSlot");
            string output = string.Join(", ", courseSection.Select(o => o.Course.CourseName + " Used on days " + o.TimeSlot.TimeSlotType));
            return output;
        }
    }
}
