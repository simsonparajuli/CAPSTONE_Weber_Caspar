using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	public class ClassroomConfiguration
	{
		[Key]
		public int ClassroomConfigID { get; set; }

		public int ClassRoomID { get; set; }

		public int ClassroomDetailID { get; set; }
	}
}
