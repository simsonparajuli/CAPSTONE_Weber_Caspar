using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CASPAR.ApplicationCore.Models
{
	public class Building

	{
		[Key]
		public int BuildingID { get; set; }

		[Required]
		[Display(Name = "Campus")]
		public int CampusID { get; set; }
		[ForeignKey("CampusID")]
		[ValidateNever]
		public Campus Campus { get; set; }

		[StringLength(30)]
		public string BuildingName { get; set; }
	}
}
