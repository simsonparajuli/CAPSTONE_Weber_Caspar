using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CASPAR.Pages
{

	public class CardExampleModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public CardExampleModel(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[BindProperty]
		public IdentityRole CurrentRole { get; set; }

		public void OnGet()
		{
			CurrentRole = new IdentityRole();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			CurrentRole.NormalizedName = CurrentRole.Name.ToUpper();

			await _roleManager.CreateAsync(CurrentRole);
			return RedirectToPage("./Index", new { success = true, message = "Successfully Added" });
		}
	}
}
