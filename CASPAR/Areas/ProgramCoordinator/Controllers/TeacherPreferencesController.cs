using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Omu.AwesomeMvc;
using CASPAR.ApplicationCore.ViewModels;

namespace CASPAR.Areas.ProgramCoordinator.Controllers
{
    public class TeacherPreferencesController : Controller
    {

        private UnitOfWork _unitOfWork;
        public TeacherPreferencesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult TeacherPref()
        {
            //return PartialView("TeacherPreferences", SemesterId + "," + CourseId) ;
            return PartialView();
            //return PartialView(new CASPAR.Pages.Shared.TeacherPreferencesModel { _SemesterId = SemesterId ?? 0, _CourseId = CourseId ?? 0 });
        }

        public IActionResult GetTeacherPrefs(GridParams g, string? SemesterAndCourseId)
        {
            var split = SemesterAndCourseId.Split(',');
            int semesterId = int.Parse(split[0]);
            int courseId = int.Parse(split[1]);


            var teacherPrefs = _unitOfWork.InstructorPreferences.GetAll(o => o.SemesterID == semesterId && o.CourseID == courseId, includes: "Modality,Campus,ApplicationUser");

            var gridModel = new GridModelBuilder<InstructorPreferences>(teacherPrefs.AsQueryable(), g);

            gridModel.KeyProp = o => o.PreferenceDetailsID;
            gridModel.Map = o => new 
            {
                o.PreferencesRank,
                Mod = o.Modality.ModalityType,
                Loc = o.Campus?.CampusName ?? "",
                Name = o.ApplicationUser.FirstName + " " + o.ApplicationUser.LastName
            };

            var jsonOut = Json(gridModel.Build());
            return jsonOut;
        }

        public IActionResult GetItem(int? v)
        {
            var o = _unitOfWork.InstructorPreferences.GetById(v);

            return Json(new KeyContent(o.WNumber, o.ApplicationUser.FirstName + " " + o.ApplicationUser.LastName));
        }
    }
}
