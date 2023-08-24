using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using EllipticCurve;
using Omu.AwesomeMvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CASPAR.Areas.Instructors.Controllers
{
    [Authorize]
    public class PreferencesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]
        public IEnumerable<InstructorPreferences> PreferenceDetails { get; set; }

        public PreferencesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

		// GET: /Preferences/DisplayInstructorPreferences
		[Authorize(Roles = StaticDetails.Instructor)]
		public IActionResult DisplayInstructorPreferences(GridParams g, int? Semester)
        {
            // Get current logged in user
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
            ApplicationUser AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            // Find all their preferences
            IEnumerable<InstructorPreferences> instructorPreferences =
                _unitOfWork.InstructorPreferences.GetAll(
                    u => u.WNumber == AppUser.Id && u.SemesterID == (Semester == null ? 0 : Semester)
				);

            // For each of the optional paramaters add them if they exist
            var list = instructorPreferences.ToList();

            // TODO: add functionality in GenericRepository to have it handle null values FK's
            for (int i = 0; i < list.LongCount(); i++)
            {
                InstructorPreferences preference = list[i];

                // Note: Modality, Semseter, Course, WNumber are not optional
                string optionalValues = string.Empty;

                if (preference.ModalityID != null)
                {
                    optionalValues += "Modality,";
                }
                if (preference.DayOfWeekID != null)
                {
                    optionalValues += "DayOfWeek,";
                }
                if (preference.TimeOfDayID != null)
                {
                    optionalValues += "TimeOfDay,";
                }
                if (preference.CampusID != null)
                {
                    optionalValues += "Campus,";
                }
                if (preference.WNumber != null)
                {
                    optionalValues += "ApplicationUser,";
                }
                if (preference.CourseID != null)
                {
                    optionalValues += "Course,";
                }
                if (preference.SemesterID != null)
                {
                    optionalValues += "Semester,";
                }

                // Chop of trailing comma
                if (optionalValues.Length > 0)
                {

                    optionalValues = optionalValues.TrimEnd(',');
                    list[i] = _unitOfWork.InstructorPreferences.Get(
                        u => u.PreferenceDetailsID == preference.PreferenceDetailsID,
                        includes: optionalValues);
                }
            }

            return Json(new GridModelBuilder<InstructorPreferences>(list.AsQueryable().OrderBy(u => u.PreferencesRank), g)
            {
                Map = o =>
                {
                    return new
                    {
                        o.PreferenceDetailsID,
                        o.PreferencesRank,
                        ProgramPrefix = o.Course.ProgramPrefix,
                        CourseNumber = o.Course.CourseNumber,
                        CreditHours = o.Course.CreditHours,
                        o.SafeDayOfWeekDay,
                        o.SafeTimeOfDayTime,
                        Modality = o.Modality.ModalityType,
                        o.SafeCampusName
                    };
                }
			}.Build());
        }

		// GET: /Preferences/DisplayStudentPreferences
		[Authorize(Roles = StaticDetails.Student)]
		public IActionResult DisplayStudentPreferences(GridParams g, int? Semester)
		{
			// Get current logged in user
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			ApplicationUser AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			// Find all their preferences
			IEnumerable<StudentPreferences> studentPreferences =
				_unitOfWork.StudentPreferences.GetAll(
					u => u.WNumber == AppUser.Id && u.SemesterID == (Semester == null ? 0 : Semester)
				);

			// For each of the optional paramaters add them if they exist
			var list = studentPreferences.ToList();

			// TODO: add functionality in GenericRepository to have it handle null values FK's
			for (int i = 0; i < list.LongCount(); i++)
			{
				StudentPreferences preference = list[i];

				// Note: Modality, Semseter, Course, WNumber are not optional
				string optionalValues = string.Empty;

				if (preference.ModalityID != null)
				{
					optionalValues += "Modality,";
				}
				if (preference.DayOfWeekID != null)
				{
					optionalValues += "DayOfWeek,";
				}
				if (preference.TimeOfDayID != null)
				{
					optionalValues += "TimeOfDay,";
				}
				if (preference.CampusID != null)
				{
					optionalValues += "Campus,";
				}
				if (preference.WNumber != null)
				{
					optionalValues += "ApplicationUser,";
				}
				if (preference.CourseID != null)
				{
					optionalValues += "Course,";
				}
				if (preference.SemesterID != null)
				{
					optionalValues += "Semester,";
				}

				// Chop of trailing comma
				if (optionalValues.Length > 0)
				{

					optionalValues = optionalValues.TrimEnd(',');
					list[i] = _unitOfWork.StudentPreferences.Get(
						u => u.PreferenceDetailsID == preference.PreferenceDetailsID,
						includes: optionalValues);
				}
			}

			return Json(new GridModelBuilder<StudentPreferences>(list.AsQueryable(), g)
			{
				Map = o =>
				{
					return new
					{
						o.PreferenceDetailsID,
						ProgramPrefix = o.Course.ProgramPrefix,
						CourseNumber = o.Course.CourseNumber,
						CreditHours = o.Course.CreditHours,
						o.SafeTimeOfDayTime,
						Modality = o.Modality.ModalityType,
						o.SafeCampusName
					};
				}
			}.Build());
		}

        // GET: /Instructor/Preferences/GetCourseList
        [Authorize(Roles = StaticDetails.Instructor + "," + StaticDetails.Student)]
        public IActionResult GetCourseList(GridParams g, string? CoursePrefix, string? CourseNumber, string? CourseName)
        {

            // If nothing is provided submit all
            IEnumerable<Course> courses = _unitOfWork.Course.GetAll();

			if (CoursePrefix != null)
            {
                courses = courses.Where(x => x.ProgramPrefix == CoursePrefix);
            }

            if (CourseNumber != null)
            {
                courses = courses.Where(x => x.CourseNumber == CourseNumber);
            }

            if (CourseName != null)
            {
                courses = courses.Where(x => x.CourseName.IndexOf(CourseName, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return Json(new GridModelBuilder<Course>(courses.AsQueryable().OrderBy(u => u.ProgramPrefix).ThenBy(u => Convert.ToInt32(u.CourseNumber)).ThenBy(u => u.CourseName), g)
            {
				Map = o =>
				{
					return new
					{
                        o.CourseID,
                        o.ProgramPrefix,
						o.CourseNumber,
						o.CourseName,
                        o.CreditHours,
					};
				},
                Key = "CourseID",
				DefaultKeySort = Sort.None
			}.Build());
		}
    }
}