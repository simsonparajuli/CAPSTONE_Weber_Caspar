using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Ticket
	{
		[Key]
		public int TicketID { get; set; }

		[Required]
		public string WNumber { get; set; }

		[Required]
		[ForeignKey("WNumber")]
		public Instructor Instructor { get; set;}

		[Required]
		public string TicketType { get; set; }

		public string? TicketDescription { get; set; }
	}
}
