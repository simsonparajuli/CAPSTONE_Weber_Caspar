using CASPAR.DataAccess;
using CASPAR.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.Administrator.Pages.Semester_Template
{
	public class IndexModel : PageModel
	{

		private readonly UnitOfWork _UnitOfWork;  
		public IEnumerable<Template> objTemplateList;  
		
		public IndexModel(UnitOfWork unitOfWork)  //dependency injection of database service
		{
			_UnitOfWork = unitOfWork;
		}
		public IActionResult OnGet()
		{
			objTemplateList = _UnitOfWork.Template.GetAll();
			return Page();
		}
	}
}
