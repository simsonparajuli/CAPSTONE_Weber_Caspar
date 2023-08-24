using CASPAR.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.ApplicationCore.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">Main type</typeparam>
	public interface IPreferencesRepository<T> : IGenericRepository<PreferenceDetails>
	{
		/// <summary>
		/// Update all ranks below index by +1 so the new rank can be inserted at the correct rank
		/// </summary>
		/// <param name="index">The rank of what the wants the new preference to be</param>
		public void addPreferencesRank(InstructorPreferences instructorPreferences);

		/// <summary>
		/// Update a preference rank so that it can be moved in the ranking
		/// </summary>
		/// <param name="instructorPreferences"></param>
		public void updatePreferencesRank(InstructorPreferences instructorPreferences);

		/// <summary>
		/// Update a preference rank so that it can be moved in the ranking
		/// </summary>
		/// <param name="instructorPreferences"></param>
		public void deletePreferencesRank(InstructorPreferences instructorPreferences);

	}
}
