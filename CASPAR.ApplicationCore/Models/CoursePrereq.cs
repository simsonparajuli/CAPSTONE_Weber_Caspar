using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class CoursePrereq
	{
		[Key]
		[Required]
		public int CoursePrereqID { get; set; }

		[Required]
		[ForeignKey("CourseID")]
		public int CourseID { get; set; }

		public string PrereqNotes { get; set; }
	}
}
