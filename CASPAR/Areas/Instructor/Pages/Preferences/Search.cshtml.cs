using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.Instructors.Pages.Preferences
{
	[Authorize(Roles = StaticDetails.Instructor + "," + StaticDetails.Student)]
	public class SearchModel : PageModel
    {
        private UnitOfWork _unitOfWork;

        [BindProperty(SupportsGet = true)]
        public CourseSearchVM CourseSearchVM { get; set; }

        public SearchModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public void OnGet(int? Semester)
        {
            if (CourseSearchVM.Course == null)
            {
                CourseSearchVM.Course = new Course();
            }

            if(Semester != null)
            {
				CourseSearchVM.Semester = new Semester();
				CourseSearchVM.Semester = _unitOfWork.Semester.Get(u => u.Id == Semester);
			}
            else if (CourseSearchVM.Semester == null)
            {
                CourseSearchVM.Semester = new Semester();
            }
        }
    }
}
