using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Student : ApplicationUser
	{
		[Required]
		public string ClassStanding { get; set; }

		[Required]
		public int MajorId { get; set; }
		public int? GraduationYear { get; set; }

		[ForeignKey(nameof(MajorId))]
		public Major Major { get; set; }
	}
}
