using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.Utility
{
	public static class SDTicketType
	{
		public static string Error = "Error";
		public static string TimeOff = "Time Off";
		public static string General = "General";

		public static List<string> getAllTypes()
		{
			List<string> list = new List<string>();

			list.Add(Error);
			list.Add(TimeOff);
			list.Add(General);

			return list;
		}
	}
}
