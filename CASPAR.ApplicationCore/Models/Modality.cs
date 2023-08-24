using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class Modality
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string ModalityType { get; set; }

		public string? ModalityDescription { get; set; }
	}
}
