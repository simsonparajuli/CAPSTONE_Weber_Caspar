using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.ProgramCoordinator.Pages.Tickets
{
	[Authorize(Roles = StaticDetails.PC)]
	public class IndexModel : PageModel
    {
		[Authorize(Roles = StaticDetails.PC)]
		public void OnGet()
        {
        }
    }
}
