using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class TimeOfDay
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Time { get; set; }
	}
}
