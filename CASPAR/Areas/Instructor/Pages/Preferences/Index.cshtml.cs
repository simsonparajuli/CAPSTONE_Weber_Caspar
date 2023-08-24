using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Claims;

namespace CASPAR.Areas.Instructors.Pages.Preferences
{
	[Authorize(Roles = StaticDetails.Instructor + "," + StaticDetails.Student)]
	public class IndexModel : PageModel
    {
        private UnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;


		[BindProperty(SupportsGet = true)]
        public Semester Semester { get; set; }

        public IEnumerable<Semester> Semesters;

		[BindProperty(SupportsGet = true)]
		public InstructorLoad? InstructorLoad { get; set; }

		[BindProperty(SupportsGet = true)]
		public ApplicationUser AppUser { get; set; }


		public IndexModel(UnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
        public void OnGet(int? Semester)
        {
			Semesters = new List<Semester>();
            Semesters = _unitOfWork.Semester.GetAll();

			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

			if (Semester != null)
            {
                this.Semester = Semesters.FirstOrDefault(u => u.Id == Semester);
                this.InstructorLoad = _unitOfWork.InstructorLoad.Get(u => u.SemesterID == Semester && u.WNumber == AppUser.Id);
			}

        }
    }
}
