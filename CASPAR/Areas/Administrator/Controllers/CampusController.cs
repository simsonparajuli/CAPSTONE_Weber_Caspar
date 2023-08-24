using CASPAR.ApplicationCore.Models;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omu.AwesomeMvc;

namespace CASPAR.Areas.Administrator.Controllers
{
	[Authorize(Roles = StaticDetails.Admin)]
	public class CampusController : Controller
	{
		private readonly UnitOfWork _unitOfWork;

		[BindProperty]
		public IEnumerable<Ticket> Tickets { get; set; }

		public CampusController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// GET: /Tickets/GetAll
		public IActionResult GetAll(GridParams g)
		{
			List<Campus> query = _unitOfWork.Campus.GetAll().ToList();
			List<SafeCampus> safe = new List<SafeCampus>();
			foreach(Campus c in query)
			{
				safe.Add(new SafeCampus(c));
			}

			return Json(new GridModelBuilder<SafeCampus>(safe.AsQueryable(), g)
			{
				Map = o =>
				{
					return new
					{
						o.Campus.Id,
						o.Campus.CampusName,
						o.Campus.Address1,
						o.SafeAddress2,
						// If their is a second address add a command to seperate them
						Address = o.Campus.Address1 + (o.SafeAddress2 != "" ? "," + o.SafeAddress2 : o.SafeAddress2),
						o.Campus.City,
						o.Campus.Country,
						o.Campus.State,
						o.Campus.Zip
					};
				}
			}.Build());
		}
	}
}
