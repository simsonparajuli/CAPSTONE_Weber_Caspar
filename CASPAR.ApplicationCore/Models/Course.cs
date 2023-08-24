using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Course
	{
		[Key]
		public int CourseID { get; set; }

		[Required]
		public string ProgramPrefix { get; set; }

		[Required]
		public string CourseNumber { get; set; }

		[Required]
		public string CourseName { get; set; }

		[Required]
		public int CreditHours{ get; set; }

		[Required]
		public string CourseDescription { get; set; }
	}
}
