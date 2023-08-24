using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.Administrator.Pages.Campuses
{
	[Authorize(Roles = StaticDetails.Admin)]
	public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
