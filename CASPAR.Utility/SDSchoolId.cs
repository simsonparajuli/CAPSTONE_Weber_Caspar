using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.Utility
{
	public static class SDSchoolId
	{
		private static string _SchooldId = "W00000000";

		/// <summary>
		/// Get the current SchoolId
		/// </summary>
        public static string SchoolId
		{
			get
			{
				return _SchooldId;
			}
		}

		/// <summary>
		/// Increment a number with the SchoolId format and return the new number 
		/// </summary>
		/// <param name="WNumber"></param>
		/// <returns></returns>
		private static String IncrementWNumber(String WNumber)
		{
			int WNumberAsNumber;

			// Create a string of 0's for the lenght of the Wnumber W12345678 -> 00000000
			String format = new string('0', 8);

			// Try to parse the Wnumber, Also remove the staring W
			Int32.TryParse(WNumber.Substring(1), out WNumberAsNumber);
			WNumberAsNumber++;

			// Rebuild the WNumber
			return WNumber[0] + WNumberAsNumber.ToString(format);
		}

		/// <summary>
		/// Generate a new SchoolId Number. FOR SeedData ONLY.
		/// </summary>
		/// <returns></returns>
		public static String GenerateNewSchoolIdNumber()
		{
			string newSchoolId = IncrementWNumber(_SchooldId);
			_SchooldId = newSchoolId;
			return newSchoolId;
		}
	}
}
