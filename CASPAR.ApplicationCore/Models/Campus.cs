using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Campus
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string CampusName { get; set; }

		[Required]
		public string Address1 { get; set; }

		public string? Address2 { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string Country { get; set; }

		[Required]
		public string State { get; set; }

		[Required]
		public string Zip { get; set; }
	}
}
