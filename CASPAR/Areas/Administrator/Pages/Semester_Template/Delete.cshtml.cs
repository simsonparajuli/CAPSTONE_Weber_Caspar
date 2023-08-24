using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.Administrator.Pages.Semester_Template
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public Template objTemplate { get; set; }


        public DeleteModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            objTemplate = new Template();

            objTemplate = _unitOfWork.Template.GetById(id);

            if (objTemplate == null)
            {
                return NotFound();
            }

            //create new mode
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Template.Delete(objTemplate);  // Adds to memory
            TempData["success"] = "Template deleted Successfully";

            _unitOfWork.Commit(); // Saves to database

            return RedirectToPage("./Index");
        }
    }
}
