using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPAR.Areas.Administrator.Pages.Semester_Template
{
    public class UpsertModel : PageModel
    {
		private readonly UnitOfWork _unitOfWork;


        [BindProperty]
        public TemplateVM TemplateVM { get; set; }
		
        public IEnumerable<SemesterTemplate> SemesterTemplateList;
        public List<Course> CourseList { get; set; }

		public UpsertModel(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult OnGet(int? id)
		{
			TemplateVM = new TemplateVM();
			TemplateVM.Template = new Template();
			TemplateVM.SemesterTemplate = new SemesterTemplate();
			//TemplateVM.SemesterTemplate = null;

            SemesterTemplateList = new List<SemesterTemplate>();

			if (id == null || id == 0) // create mode
			{
				TemplateVM.Template = new Template();
				TemplateVM.Template.TemplateID = 0;
				return Page();
			}

            SemesterTemplateList = _unitOfWork.SemesterTemplate.GetAll(s => s.TemplateID == id);

			CourseList = _unitOfWork.Course.GetAll().ToList();

			//edit mode
			if (id != 0)
			{
				TemplateVM.Template = _unitOfWork.Template.GetById(id);
			}

			if (TemplateVM.Template == null)
			{
				return NotFound();
			}

			//create new mode
			return Page();
		}

		public IActionResult OnPostTemplate(int? id)
		{

            //if this is a new template
            if (TemplateVM.Template.TemplateID == 0)
			{
				_unitOfWork.Template.Add(TemplateVM.Template);  // Adds to memory
				TempData["success"] = "Template added Successfully";
            }
			//if schedule exists
			else
			{
				_unitOfWork.Template.Update(TemplateVM.Template);
				TempData["success"] = "Template updated Successfully";
			}
			_unitOfWork.Commit(); // Saves to database

			// Reload the page
			return OnGet(TemplateVM.Template.TemplateID);

        }

		public IActionResult OnPostSemesterTemplate(int? id)
        {

            //if this is a new SemesterTemplate
            if (TemplateVM.SemesterTemplate.SemesterTemplateID == 0)
			{
				_unitOfWork.SemesterTemplate.Add(TemplateVM.SemesterTemplate);  // Adds to memory
				TempData["success"] = "SemesterTemplate added Successfully";
			}
			//if schedule exists
			else
			{
				_unitOfWork.SemesterTemplate.Update(TemplateVM.SemesterTemplate);
				TempData["success"] = "SemesterTemplate updated Successfully";
			}
			_unitOfWork.Commit(); // Saves to database

			// Reload the page
			return RedirectToPage("", new { id = id });
        }

	}
}
