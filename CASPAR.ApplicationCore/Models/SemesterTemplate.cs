using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class SemesterTemplate
	{
		[Key]
		[Required]
		public int SemesterTemplateID { get; set; }

		[Required]
		public int CourseID { get; set; }

		[Required]
		[ForeignKey("CourseID")]
		public Course Course { get; set; }

		[Required]
		public int TemplateID { get; set; }

		[Required]
		[ForeignKey("TemplateID")]
		[ValidateNever]
		public Template Template { get; set; }

		[Required]
		public int CourseQuantity { get; set; }

	}
}
