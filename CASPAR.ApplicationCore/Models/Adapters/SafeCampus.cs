using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Models
{
	/// <summary>
	/// Designed to return safe / non-null values of a given class
	/// </summary>
	public class SafeCampus
	{
		public Campus Campus;

        public SafeCampus(Campus campus)
        {
			Campus = campus;
		}

		public string? SafeAddress2 { get { return Campus.Address2 != null ? Campus.Address2 : ""; } }
	}
}
