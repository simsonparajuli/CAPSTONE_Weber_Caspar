using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace CASPAR.Areas.Students.Pages.Information
{
	[Authorize(Roles = StaticDetails.Student)]
	public class IndexModel : PageModel
    {

        [BindProperty]
		public Student Student { get; set; }

		private UnitOfWork _unitOfWork;

		public IEnumerable<Major> Majors { get; set; }

        public IndexModel(UnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
			//StudentInformationVM = new StudentInformationVM();
		}

		public IActionResult OnGet()
        {
			// Get current logged in user
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			Student AppUser = _unitOfWork.Student.Get(u => u.Id == userId, includes:"Major");
			Student = AppUser;


			if (Majors == null)
			{
				Majors = new List<Major>();
				Majors = _unitOfWork.Major.GetAll();
			}

			// Look for the major with the same Id as the application user
			//foreach(SelectListItem item in Majors )
			//{
			//	if( Int32.Parse(item.Value) == AppUser.MajorId)
			//	{
			//		item.Selected = true;
			//	}
			//}
			return Page();
        }

		[HttpPost]
		public IActionResult OnPostEditInformation()
		{
			// use a temp to store the information we want to update
			Student tempStudent = Student;

			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			Student AppUser = _unitOfWork.Student.Get(u => u.Id == userId, includes: "Major");

			Student = AppUser;
			Student.GraduationYear = tempStudent.GraduationYear;
			Student.MajorId = tempStudent.MajorId;

			_unitOfWork.Student.Update(Student);
			return RedirectToPage();
		}
    }
}
