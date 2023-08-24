using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class DayOfTheWeek

	{
		[Key]
		public int DayOfTheWeekID { get; set; }

		[StringLength(25)]
		public string DayOfWeek { get; set; }
	}
}
