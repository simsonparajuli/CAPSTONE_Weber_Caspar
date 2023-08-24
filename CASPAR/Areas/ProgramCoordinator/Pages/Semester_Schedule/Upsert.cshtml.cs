using CASPAR.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CASPAR.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CASPAR.Areas.ProgramCoordinator.Pages.Semester_Schedule
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        [BindProperty]
        public SemesterSchedule objSemesterSchedule { get; set; }

		public IEnumerable<SelectListItem> SemesterTemplateList { get; set; }
		public IEnumerable<SelectListItem> StatusList { get; set; }
        public IEnumerable<SelectListItem> SemesterList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet(int? id)
        {
            objSemesterSchedule = new SemesterSchedule();
			SemesterTemplateList = _unitOfWork.SemesterTemplate.GetAll().Select(s => new SelectListItem
			{
				Text = s.Template.TemplateName,
				Value = s.SemesterTemplateID.ToString()
			});
			StatusList = _unitOfWork.Status.GetAll().Select(s => new SelectListItem
            {
                Text = s.StatusType,
                Value = s.StatusID.ToString()
            });
            SemesterList = _unitOfWork.Semester.GetAll().Select(s => new SelectListItem
            {
                Text = s.SemesterName+ " " + s.SemesterYear,
                Value = s.Id.ToString()
            });

            if (id == null || id == 0) // create mode
            {
                return Page();
            }

            //edit mode
            if (id != 0)
            {
                objSemesterSchedule = _unitOfWork.SemesterSchedule.GetById(id);
            }

            if (objSemesterSchedule == null)
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

            //if this is a new schedule
            if (objSemesterSchedule.SemesterScheduleID == 0)
            {
                _unitOfWork.SemesterSchedule.Add(objSemesterSchedule);  // Adds to memory
                TempData["success"] = "Semester Schedule added Successfully";
            }
            //if schedule exists
            else
            {
                _unitOfWork.SemesterSchedule.Update(objSemesterSchedule);
                TempData["success"] = "Semester Schedule updated Successfully";
            }
            _unitOfWork.Commit(); // Saves to database

            return RedirectToPage("./Upsert");
        }
    }
}
