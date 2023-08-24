using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CASPAR.ApplicationCore.Models
{
	public class ClassroomDetail
	{
		[Key]
		public int ClassroomDetailID { get; set; }

		[StringLength(50)]
		public string ClassroomDetails { get; set; }

		[StringLength(200)]
		public string ClassroomDetailDescrip { get; set; }
	}
}
