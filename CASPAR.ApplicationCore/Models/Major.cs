using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Major
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string MajorName { get; set; }
	}
}
