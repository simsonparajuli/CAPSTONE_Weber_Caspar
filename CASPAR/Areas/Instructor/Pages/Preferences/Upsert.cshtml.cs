using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CASPAR.Areas.Instructors.Pages.Preferences
{
	[Authorize(Roles = StaticDetails.Instructor + "," + StaticDetails.Student)]
	public class UpsertModel : PageModel
    {
        private UnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public InstructorPreferenceAddVM InstructorPreferenceAddVM { get; set; }

        public List<TimeOfDay> TimeOfTheDay { get; set; }
        public List<DayOfTheWeek> DayOfTheWeek { get; set; }
        public List<Modality> Modalities { get; set; }
        public List<Campus> Campuses { get; set; }
        public List<InstructorPreferences> PreviousPreferences { get; set; }
		public int LowestModelPreferenceValue { get; set; }

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
            InstructorPreferenceAddVM = new InstructorPreferenceAddVM();
            if (PreferenceID != null)
            {
                InstructorPreferences ip = _unitOfWork.InstructorPreferences.GetById(PreferenceID);
                InstructorPreferenceAddVM.Preference = ip;

                InstructorPreferenceAddVM.Course = new Course();
                InstructorPreferenceAddVM.Course = _unitOfWork.Course.Get(u => u.CourseID == ip.CourseID);
                CourseID = ip.CourseID;

                InstructorPreferenceAddVM.Semester = new Semester();
                InstructorPreferenceAddVM.Semester = _unitOfWork.Semester.Get(u => u.Id == ip.SemesterID);
                SemesterID = ip.SemesterID;
            }
            else if (InstructorPreferenceAddVM.Preference == null)
            {
                InstructorPreferenceAddVM.Preference = new InstructorPreferences();

                InstructorPreferenceAddVM.Course = new Course();
                InstructorPreferenceAddVM.Course = _unitOfWork.Course.Get(u => u.CourseID == CourseID);

                InstructorPreferenceAddVM.Semester = new Semester();
                InstructorPreferenceAddVM.Semester = _unitOfWork.Semester.Get(u => u.Id == SemesterID);
            }

            // Get all for selects
            TimeOfTheDay = new List<TimeOfDay>();
            TimeOfTheDay = _unitOfWork.TimeOfDay.GetAll().ToList();

            DayOfTheWeek = new List<DayOfTheWeek>();
            DayOfTheWeek = _unitOfWork.DAY_OF_WEEK.GetAll().ToList();

            Modalities = new List<Modality>();
            Modalities = _unitOfWork.Modality.GetAll().ToList();

            Campuses = new List<Campus>();
            Campuses = _unitOfWork.Campus.GetAll().ToList();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
            Instructor AppUser = _unitOfWork.Instructor.Get(u => u.Id == userId);

            PreviousPreferences = new List<InstructorPreferences>();
            PreviousPreferences = _unitOfWork.InstructorPreferences.GetAll(u => u.WNumber == AppUser.Id, orderBy: u => u.PreferencesRank, includes: "Course").ToList();
            PreviousPreferences.RemoveAll(u => u.SemesterID != SemesterID);

            // Add the userId to the from
            InstructorPreferenceAddVM.Preference.WNumber = AppUser.Id;

			LowestModelPreferenceValue = PreviousPreferences.Count + 1; // Get the lowest and then one, or if none just one

			return Page();
        }

        [HttpPost]
        public IActionResult OnPostCreatePreference()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
            Instructor AppUser = _unitOfWork.Instructor.Get(u => u.Id == userId);

            // Set Preference equal to the other values
            InstructorPreferenceAddVM.Preference.WNumber = AppUser.Id;
            InstructorPreferenceAddVM.Preference.SemesterID = InstructorPreferenceAddVM.Semester.Id;
            InstructorPreferenceAddVM.Preference.CourseID = InstructorPreferenceAddVM.Course.CourseID;

            _unitOfWork.Preferences.addPreferencesRank(InstructorPreferenceAddVM.Preference);
            _unitOfWork.Commit();

            return RedirectToPage("/Preferences/Index", new { Semester = InstructorPreferenceAddVM.Semester.Id});
        }

        [HttpPost]
        public IActionResult OnPostEditPreference()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
            Instructor AppUser = _unitOfWork.Instructor.Get(u => u.Id == userId);

            // Set Preference equal to the other values
            InstructorPreferenceAddVM.Preference.SemesterID = InstructorPreferenceAddVM.Semester.Id;
            InstructorPreferenceAddVM.Preference.CourseID = InstructorPreferenceAddVM.Course.CourseID;

            _unitOfWork.Preferences.updatePreferencesRank(InstructorPreferenceAddVM.Preference);
            _unitOfWork.Commit();
            //_unitOfWork.InstructorPreferences.Update(InstructorPreferenceAddVM.Preference);
            //_unitOfWork.Commit();

            return RedirectToPage("/Preferences/Index", new { Semester = InstructorPreferenceAddVM.Semester.Id });
		}

        [HttpPost]
        public IActionResult OnPostDeletePreference()
        {
            _unitOfWork.Preferences.deletePreferencesRank(InstructorPreferenceAddVM.Preference);
            _unitOfWork.Commit();

            return RedirectToPage("/Preferences/Index", new { Semester = InstructorPreferenceAddVM.Semester.Id });
		}

    }
}
