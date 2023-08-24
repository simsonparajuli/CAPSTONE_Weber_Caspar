using CASPAR.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.ViewModels
{
	public class CourseSearchVM
	{
		public Semester Semester { get; set; }

		public Course Course { get; set; }
	}
}
