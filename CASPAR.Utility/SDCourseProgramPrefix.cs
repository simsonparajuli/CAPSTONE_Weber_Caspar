using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CASPAR.Utility
{
	public static class SDCourseProgramPrefix
	{
		public const string CS = "CS";
		public const string NET = "NET";

		public static List<string> getAll()
		{
			List<string> list = new List<string>
			{
				CS,
				NET
			};

			return list;
		}
	}
}
