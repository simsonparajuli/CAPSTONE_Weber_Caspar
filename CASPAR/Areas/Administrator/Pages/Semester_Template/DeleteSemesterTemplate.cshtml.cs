using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.Administrator.Pages.Semester_Template
{
    public class DeleteSemesterTemplateModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterTemplate objSemesterTemplate { get; set; }

        public Course objCourse { get; set; }
        public DeleteSemesterTemplateModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            objSemesterTemplate = new SemesterTemplate();

            objSemesterTemplate = _unitOfWork.SemesterTemplate.GetById(id);

            if (objSemesterTemplate == null)
            {
                return NotFound();
            }

            _unitOfWork.SemesterTemplate.Delete(objSemesterTemplate);  // Adds to memory
            TempData["success"] = "SemesterTemplate deleted Successfully";

            _unitOfWork.Commit(); // Saves to database

            return RedirectToPage("./Upsert", new { id = objSemesterTemplate.TemplateID });
        }
    }
}
