using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CASPAR.Areas.ProgramCoordinator.Pages.CourseSection
{
    public class IndexModel : PageModel
    {
        public int? ClassroomID { get; set; }
        public int? InstructorID { get; set; }
        public void OnGet()
        {
        }
    }
}
