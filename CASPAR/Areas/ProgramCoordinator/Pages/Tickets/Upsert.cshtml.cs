using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CASPAR.Areas.ProgramCoordinator.Pages.Tickets
{
	[Authorize]
	public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		[BindProperty]
		public TicketVM TicketVM { get; set; }

        public UpsertModel(UnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
			_userManager = userManager;
			_roleManager = roleManager;
		}


		[Authorize(Roles = StaticDetails.PC + "," + StaticDetails.Instructor)]
		public IActionResult OnGet(int? id)
        {

			TicketVM = new TicketVM()
			{
				Ticket = new Ticket(),
				Instructor = new Instructor(),
			};

			//edit mode
			if (id != 0)
			{
				TicketVM.Ticket = _unitOfWork.Tickets.GetById(id);
				TicketVM.Instructor = _unitOfWork.Instructor.Get(i => i.Id == TicketVM.Ticket.WNumber);
			}
			else
			{
				string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
				Instructor AppUser = _unitOfWork.Instructor.Get(u => u.Id == userId);

				TicketVM.Instructor = AppUser;
			}

			//if (objTicket == null)
			//{
			//	return NotFound();
			//}

			//create new mode
			return Page();
		}


		[HttpPost]
		[Authorize(Roles = StaticDetails.PC + "," + StaticDetails.Instructor)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostCreateTicket()
		{
			Instructor? objInstructor = TicketVM.Instructor;
			Ticket objTicket = TicketVM.Ticket;

			// If it is an admin creating the ticket find who they are looking for
			if(objInstructor.Id == null)
			{
				objInstructor = _unitOfWork.Instructor.Get(u => u.WNumber == objInstructor.WNumber);
				// Check if the objInstructor exists
				if(objInstructor == null)
				{
					//TempData["error"] = "Error with WNumber.";
					return RedirectToPage("Upsert", new { Id = 0 });
				}
			}


			objTicket.WNumber = objInstructor.Id;

			_unitOfWork.Tickets.Add(objTicket);
			_unitOfWork.Commit();

			// Get current logged in user
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's Id
			ApplicationUser AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
			var roles =  await _userManager.GetRolesAsync(AppUser);

			// IF PC return to ticket index
			if (roles.Contains(StaticDetails.PC))
			{
				return RedirectToPage("./Index");
			}
			// Else return to the main index page
			else
			{
				return RedirectToPage("/Index");
			}
		}


		[HttpPost]
		[Authorize(Roles = StaticDetails.PC)]
		[ValidateAntiForgeryToken]
		public IActionResult OnPostEditTicket()
		{
			//return Page();
			Ticket ticket = TicketVM.Ticket;

			_unitOfWork.Tickets.Update(ticket);
			_unitOfWork.Commit();

			return RedirectToPage("./Index");
		}


		[HttpPost]
		[Authorize(Roles = StaticDetails.PC)]
		[ValidateAntiForgeryToken]
		public IActionResult OnPostDeleteTicket()
		{
			//return Page();
			Ticket ticket = TicketVM.Ticket;

			_unitOfWork.Tickets.Delete(ticket);
			_unitOfWork.Commit();

			return RedirectToPage("./Index");
		}
	}
}
