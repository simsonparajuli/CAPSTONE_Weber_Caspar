using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class InstructorLoad
	{
		[Key]
		public int InstructorLoadID { get; set; }

		[Required]
		public String WNumber { get; set; }

		[Required]
		[ForeignKey("WNumber")]
		[ValidateNever]
		public Instructor Instructor { get; set; }

		[Required]
		public int SemesterID { get; set; }

		[Required]
		[ForeignKey("SemesterID")]
		[ValidateNever]
		public Semester Semester { get; set; }

		[Required]
		public int Load { get; set; }
	}
}
