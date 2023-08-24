using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.Administrator.Pages.Campuses
{
	[Authorize(Roles = StaticDetails.Admin)]
    public class UpsertModel : PageModel
    {
		private readonly UnitOfWork _unitOfWork;

		[BindProperty]
		public Campus Campus { get; set; }

		public UpsertModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int? ID)
        {
			Campus = new Campus();
			if(ID != 0 && ID != null)
			{
				Campus = _unitOfWork.Campus.GetById(ID);
			}
		}

		[HttpPost]
		public IActionResult OnPostCreateCampus()
		{
			_unitOfWork.Campus.Add(Campus);
			return RedirectToPage("./Index");
		}

		[HttpPost]
		public IActionResult OnPostEditCampus()
		{
			_unitOfWork.Campus.Update(Campus);

			return RedirectToPage("./Index");
		}

		[HttpPost]
		public IActionResult OnPostDeleteCampus()
		{
			_unitOfWork.Campus.Delete(Campus);

			return RedirectToPage("./Index");
		}

	}
}
