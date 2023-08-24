using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Omu.AwesomeMvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CASPAR.Areas.ProgramCoordinator.Controllers
{
	[Authorize(Roles = StaticDetails.PC)]
	public class TicketsController : Controller
	{
		private readonly UnitOfWork _unitOfWork;

		[BindProperty]
		public IEnumerable<Ticket> Tickets { get; set; }

		public TicketsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
		{
			return View();
		}

		// GET: /Tickets/GetAll
		public IActionResult GetAll(GridParams g)
		{
			List<Ticket> query = _unitOfWork.Tickets.GetAll(includes: "Instructor").ToList();

			return Json(new GridModelBuilder<Ticket>(query.AsQueryable(), g)
			{
				Map = o =>
				{
					return new
					{
						o.TicketID,
						Name = o.Instructor.FirstName + " " + o.Instructor.LastName,
						o.TicketType,
						o.TicketDescription,
					};
				}
			}.Build());
		}
	}
}
