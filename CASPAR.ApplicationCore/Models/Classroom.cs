using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Classroom
	{
		[Key]
		public int ClassRoomID { get; set; }

		[Required]
		[Display(Name = "Building")]
		public int BuildingID { get; set; }

		[ForeignKey("BuildingID")]
		[ValidateNever]
		public Building Building { get; set; } // Might be useful

		public int RoomNum { get; set; }

		public int Capacity { get; set; }
	}
}
