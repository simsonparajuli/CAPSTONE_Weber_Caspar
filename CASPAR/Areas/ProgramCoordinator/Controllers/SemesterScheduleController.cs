using CASPAR.ApplicationCore.Interfaces;
using CASPAR.ApplicationCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class SemesterScheduleController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public SemesterScheduleController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var schedules = _unitOfWork.SemesterSchedule.GetAll().ToList();
        var templates = _unitOfWork.SemesterTemplate.GetAll().ToList();
        var semesters = _unitOfWork.Semester.GetAll().ToList();

        var viewModel = from s in schedules
                        join t in templates on s.SemesterTemplateID equals t.SemesterTemplateID
                        join sm in semesters on s.SemesterID equals sm.Id
                        select new SemesterScheduleViewModel
                        {
                            ScheduleName = s.ScheduleName,
                            Semester = $"{sm.SemesterName} {sm.SemesterYear}",
                            TemplateName = t.Template.TemplateName,
                            CourseQuantity = t.CourseQuantity,
                            OpenEnrollmentDate = s.OpenEnrollmentDate,
                            CloseEnrollmentDate = s.CloseEnrollmentDate,
                            Status = _unitOfWork.Status.Get(status => status.StatusID == s.StatusID).StatusType,
                            Course = _unitOfWork.Course.Get(course => course.CourseID == t.CourseID).CourseName
                        };

        return View(viewModel);
    }
}