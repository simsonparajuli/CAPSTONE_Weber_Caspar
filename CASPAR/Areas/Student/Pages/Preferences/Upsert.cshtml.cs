using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CASPAR.Areas.Students.Pages.Preferences
{
	[Authorize(Roles = StaticDetails.Student)]
	public class UpsertModel : PageModel
	{
		private UnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		[BindProperty]
		public StudentPreferenceAddVM StudentPreferenceAddVM { get; set; }

		public List<TimeOfDay> TimeOfTheDay { get; set; }
		public List<Modality> Modalities { get; set; }
		public List<Campus> Campuses { get; set; }

		public UpsertModel(UnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult OnGet(int? CourseID, int? SemesterID, int? PreferenceID)
		{
			// Check if we are updating or creating a new preference
			StudentPreferenceAddVM = new StudentPreferenceAddVM();
			if (PreferenceID != null)
			{
				StudentPreferences ip = _unitOfWork.StudentPreferences.GetById(PreferenceID);
				StudentPreferenceAddVM.Preference = ip;

				StudentPreferenceAddVM.Course = new Course();
				StudentPreferenceAddVM.Course = _unitOfWork.Course.Get(u => u.CourseID == ip.CourseID);
				CourseID = ip.CourseID;

				StudentPreferenceAddVM.Semester = new Semester();
				StudentPreferenceAddVM.Semester = _unitOfWork.Semester.Get(u => u.Id == ip.SemesterID);
				SemesterID = ip.SemesterID;
			}
			else if (StudentPreferenceAddVM.Preference == null)
			{
				StudentPreferenceAddVM.Preference = new StudentPreferences();

				StudentPreferenceAddVM.Course = new Course();
				StudentPreferenceAddVM.Course = _unitOfWork.Course.Get(u => u.CourseID == CourseID);

				StudentPreferenceAddVM.Semester = new Semester();
				StudentPreferenceAddVM.Semester = _unitOfWork.Semester.Get(u => u.Id == SemesterID);
			}

			// Get all for selects
			TimeOfTheDay = new List<TimeOfDay>();
			TimeOfTheDay = _unitOfWork.TimeOfDay.GetAll().ToList();

			Modalities = new List<Modality>();
			Modalities = _unitOfWork.Modality.GetAll().ToList();

			Campuses = new List<Campus>();
			Campuses = _unitOfWork.Campus.GetAll().ToList();

			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			ApplicationUser AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			return Page();
		}

		[HttpPost]
		public IActionResult OnPostCreatePreference()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			ApplicationUser AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			// Set Preference equal to the other values
			StudentPreferenceAddVM.Preference.WNumber = AppUser.Id;
			StudentPreferenceAddVM.Preference.SemesterID = StudentPreferenceAddVM.Semester.Id;
			StudentPreferenceAddVM.Preference.CourseID = StudentPreferenceAddVM.Course.CourseID;

			_unitOfWork.StudentPreferences.Add(StudentPreferenceAddVM.Preference);
			_unitOfWork.Commit();

			return RedirectToPage("/Preferences/Index", new { Area = "Instructor", Semester = StudentPreferenceAddVM.Semester.Id });
		}

		[HttpPost]
		public IActionResult OnPostEditPreference()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			ApplicationUser AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			// Set Preference equal to the other values
			StudentPreferenceAddVM.Preference.SemesterID = StudentPreferenceAddVM.Semester.Id;
			StudentPreferenceAddVM.Preference.CourseID = StudentPreferenceAddVM.Course.CourseID;

			_unitOfWork.Preferences.Update(StudentPreferenceAddVM.Preference);
			_unitOfWork.Commit();
			//_unitOfWork.InstructorPreferences.Update(InstructorPreferenceAddVM.Preference);
			//_unitOfWork.Commit();

			return RedirectToPage("/Preferences/Index", new { Area = "Instructor", Semester = StudentPreferenceAddVM.Semester.Id });
		}

		[HttpPost]
		public IActionResult OnPostDeletePreference()
		{
			_unitOfWork.StudentPreferences.Delete(StudentPreferenceAddVM.Preference);
			_unitOfWork.Commit();

			return RedirectToPage("/Preferences/Index", new { Area = "Instructor", Semester = StudentPreferenceAddVM.Semester.Id });
		}

	}
}
