using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.Models.ViewModels;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Omu.AwesomeMvc;
using CASPAR.Utility.Utils.Awesome;
using System.Linq;
using CASPAR.Utility;
using CASPAR.ApplicationCore.ViewModels;
using System.Data;

namespace CASPAR.Areas.Administrator.Controllers
{
    public class InstructorController : Controller
    {
        private UnitOfWork _unitOfWork;

        public InstructorController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult GetYears()
        {
            var years = _unitOfWork.Semester.GetAll().Select(s => s.SemesterYear).Distinct().OrderByDescending(y => y);
            return Json(years.Select(y => new KeyContent(y, y.ToString())));
        }

        public IActionResult GetInstructors(GridParams g, int year = 0)
        {
            var semesters = _unitOfWork.Semester.GetAll();

            if (year > 0)
            {
                semesters = semesters.Where(s => s.SemesterYear == year).ToList();
            }
            else
            {
                semesters = semesters.OrderByDescending(s => s.SemesterYear).ToList();
            }
            var instructors = _unitOfWork.Instructor.GetAll().ToList();
            var loads = _unitOfWork.InstructorLoad.GetAll().ToList();
            var preferences = _unitOfWork.InstructorPreferences.GetAll().ToList();
            var courses = _unitOfWork.Course.GetAll().ToList();
            //var semesters = _unitOfWork.Semester.GetAll().OrderByDescending(s => s.SemesterYear).ToList();

            var preferredCourses = from p in preferences
                                   group p by p.WNumber into preferenceGroup
                                   let maxPreference = preferenceGroup.Max(x => x.PreferencesRank)
                                   select preferenceGroup.First(x => x.PreferencesRank == maxPreference);

            var loadGroups = from l in loads
                             group l by l.WNumber into loadGroup
                             select new
                             {
                                 InstructorId = loadGroup.Key,
                                 SpringLoad = loadGroup.Where(l => l.Semester.SemesterName == SDSemesters.Spring).Sum(l => l.Load),
                                 SummerLoad = loadGroup.Where(l => l.Semester.SemesterName == SDSemesters.Summer).Sum(l => l.Load),
                                 FallLoad = loadGroup.Where(l => l.Semester.SemesterName == SDSemesters.Fall).Sum(l => l.Load),
                             };

            var result = from i in instructors
                         join pc in preferredCourses on i.Id equals pc.WNumber into prefCourseGroup
                         from pc in prefCourseGroup.DefaultIfEmpty()
                         join c in courses on pc != null ? pc.CourseID : 0 equals c.CourseID into courseGroup
                         from c in courseGroup.DefaultIfEmpty()
                         join lg in loadGroups on i.Id equals lg.InstructorId into loadGroup
                         from lg in loadGroup.DefaultIfEmpty()
                         select new InstructorViewModel
                         {
                             WNumber = i.WNumber,
                             ProgramPreference = c != null ? c.ProgramPrefix : "N/A",
                             FirstName = i.FirstName,
                             LastName = i.LastName,
                             SpringLoad = lg?.SpringLoad ?? 0,
                             SummerLoad = lg?.SummerLoad ?? 0,
                             FallLoad = lg?.FallLoad ?? 0,
                             Roles = i.InstructorType ?? "N/A",
                             Details = "Link"
                         };

            var gridModel = new GridModelBuilder<InstructorViewModel>(result.AsQueryable(), g)
            {
                KeyProp = o => o.WNumber,
                Map = o => new
                {
                    o.ProgramPreference,
                    o.FirstName,
                    o.LastName,
                    o.SpringLoad,
                    o.SummerLoad,
                    o.FallLoad,
                    o.Roles,
                    o.Details
                }
            };

            return Json(gridModel.Build());
        }

        public IActionResult GetInstructorDetails(string wNumber)
        {
            var instructors = _unitOfWork.Instructor.GetAll().ToList();
            var loads = _unitOfWork.InstructorLoad.GetAll().ToList();
            var preferences = _unitOfWork.InstructorPreferences.GetAll().ToList();
            var courses = _unitOfWork.Course.GetAll().ToList();
            var semesters = _unitOfWork.Semester.GetAll().OrderByDescending(s => s.SemesterYear).ToList();

            var preferredCourses = from p in preferences
                                   group p by p.WNumber into preferenceGroup
                                   let maxPreference = preferenceGroup.Max(x => x.PreferencesRank)
                                   select preferenceGroup.First(x => x.PreferencesRank == maxPreference);

            var loadGroups = from l in loads
                             group l by l.WNumber into loadGroup
                             select new
                             {
                                 InstructorId = loadGroup.Key,
                                 SpringLoad = loadGroup.Where(l => l.Semester.SemesterName == SDSemesters.Spring).Sum(l => l.Load),
                                 SummerLoad = loadGroup.Where(l => l.Semester.SemesterName == SDSemesters.Summer).Sum(l => l.Load),
                                 FallLoad = loadGroup.Where(l => l.Semester.SemesterName == SDSemesters.Fall).Sum(l => l.Load),
                             };

            var instructorDetail = (from i in instructors
                                    where i.WNumber == wNumber
                                    join pc in preferredCourses on i.Id equals pc.WNumber into prefCourseGroup
                                    from pc in prefCourseGroup.DefaultIfEmpty()
                                    join c in courses on pc != null ? pc.CourseID : 0 equals c.CourseID into courseGroup
                                    from c in courseGroup.DefaultIfEmpty()
                                    join lg in loadGroups on i.Id equals lg.InstructorId into loadGroup
                                    from lg in loadGroup.DefaultIfEmpty()
                                    select new InstructorDetailsViewModel
                                    {
                                        WNumber = i.WNumber,
                                        ProgramPreference = c != null ? c.ProgramPrefix : "N/A",
                                        FirstName = i.FirstName,
                                        LastName = i.LastName,
                                        SpringLoad = lg?.SpringLoad ?? 0,
                                        SummerLoad = lg?.SummerLoad ?? 0,
                                        FallLoad = lg?.FallLoad ?? 0,
                                        InstructorNotes = i.InstructorNotes
                                    }).FirstOrDefault();

            if (instructorDetail == null)
            {
                return NotFound();
            }

            return PartialView("InstructorDetailsPopupView", instructorDetail);
        }
    }
}