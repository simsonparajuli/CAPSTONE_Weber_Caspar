using CASPAR.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.ViewModels
{
	public class StudentInformationVM
	{
		public int? GraduationYear { get; set; }
		public Major Major { get; set; }
	}
}
