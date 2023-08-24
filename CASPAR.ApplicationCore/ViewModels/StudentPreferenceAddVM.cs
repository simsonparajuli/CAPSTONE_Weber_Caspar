using CASPAR.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.ViewModels
{
	public class StudentPreferenceAddVM
	{
		public Semester Semester { get; set; }

		public Course Course { get; set; }

		public StudentPreferences Preference { get; set; }

		public string? SemesterNameAndYear { get; set; }
	}
}
