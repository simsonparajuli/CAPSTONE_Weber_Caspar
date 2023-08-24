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
	public class ReleaseTime
	{
		[Key]
		public int ReleaseTimeID { get; set; }

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
		public string ReleaseTimeType { get; set; }

		[Required]
		public string ReleaseTimeNotes { get; set; }

		[Required]
		public int ReleaseTimeAmount { get; set; }
	}
}
