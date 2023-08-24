using CASPAR.ApplicationCore.Interfaces;
using CASPAR.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.DataAccess
{
	public class PreferencesRepository : GenericRepository<PreferenceDetails>, IPreferencesRepository<InstructorPreferences>
	{
		private readonly ApplicationDbContext _dbContext;

		public PreferencesRepository(ApplicationDbContext db) : base(db)
		{
			_dbContext = db;
		}

		public void addPreferencesRank(InstructorPreferences instructorPreferences)
		{
			IEnumerable<InstructorPreferences> list = _dbContext.Set<InstructorPreferences>()
				.Where(u => u.WNumber == instructorPreferences.WNumber)
				.Where(u => u.SemesterID == instructorPreferences.SemesterID)
				.Where(u => u.PreferencesRank >= instructorPreferences.PreferencesRank);


			foreach (InstructorPreferences item in list)
			{
				item.PreferencesRank += 1;
			}

			_dbContext.Set<InstructorPreferences>().Add(instructorPreferences); // Like a commit
			_dbContext.SaveChanges();
		}

		public void updatePreferencesRank(InstructorPreferences instructorPreferences)
		{
			InstructorPreferences ipOld = _dbContext.Set<InstructorPreferences>()
				.AsTracking()
				.FirstOrDefault(u => u.PreferenceDetailsID == instructorPreferences.PreferenceDetailsID);



			// If index are the same return
			if (ipOld.PreferencesRank == instructorPreferences.PreferencesRank)
			{
				_dbContext.Entry(ipOld).CurrentValues.SetValues(instructorPreferences);
				_dbContext.SaveChanges();
				return;
			}

			// Because we are inserting above the selected value we subtract one
			if (instructorPreferences.PreferencesRank != 1)
			{
				instructorPreferences.PreferencesRank--;
			}

			// If we are trying to insert one below that does not work
			if (ipOld.PreferencesRank == instructorPreferences.PreferencesRank)
			{
				_dbContext.Entry(ipOld).CurrentValues.SetValues(instructorPreferences);
				_dbContext.SaveChanges();
				return;
			}

			// If lowering the ranking of the prefernce value (1 to 4)
			if (instructorPreferences.PreferencesRank > ipOld.PreferencesRank)
			{
				IEnumerable<InstructorPreferences> list = _dbContext.Set<InstructorPreferences>()
					.Where(u => u.WNumber == ipOld.WNumber)
					.Where(u => u.SemesterID == ipOld.SemesterID)
					.Where(u => u.PreferencesRank <= instructorPreferences.PreferencesRank)
					.Where(u => u.PreferencesRank >= ipOld.PreferencesRank);
				foreach (InstructorPreferences item in list)
				{
					item.PreferencesRank -= 1;
				}
			}
			// else rasing the ranking of the prefernce value (4 to 1)
			else
			{
				IEnumerable<InstructorPreferences> list = _dbContext.Set<InstructorPreferences>()
					.Where(u => u.WNumber == ipOld.WNumber)
					.Where(u => u.SemesterID == ipOld.SemesterID)
					.Where(u => u.PreferencesRank >= instructorPreferences.PreferencesRank)
					.Where(u => u.PreferencesRank <= ipOld.PreferencesRank);
				foreach (InstructorPreferences item in list)
				{
					item.PreferencesRank += 1;
				}
			}

			//// If its the new lowest value sub one (if only 4 prefrences we pass in a 5 so need to change it to 4)
			//if(instructorPreferences.PreferencesRank > 
			//		_dbContext.Set<InstructorPreferences>()
			//		.Where(u => u.WNumber == ipOld.WNumber)
			//		.Where(u => u.SemesterID == ipOld.SemesterID).Count())
			//{
			//	instructorPreferences.PreferencesRank--;
			//}

			//ipOld.PreferencesRank = instructorPreferences.PreferencesRank;
			_dbContext.Entry(ipOld).CurrentValues.SetValues(instructorPreferences);
			_dbContext.SaveChanges();
		}

		public void deletePreferencesRank(InstructorPreferences instructorPreferences)
		{

			InstructorPreferences ipOld = _dbContext.Set<InstructorPreferences>()
				.FirstOrDefault(u => u.PreferenceDetailsID == instructorPreferences.PreferenceDetailsID);

			IEnumerable<InstructorPreferences> list = _dbContext.Set<InstructorPreferences>()
				.Where(u => u.WNumber == ipOld.WNumber)
				.Where(u => u.SemesterID == ipOld.SemesterID)
				.Where(u => u.PreferencesRank >= ipOld.PreferencesRank);

			foreach (InstructorPreferences item in list)
			{
				item.PreferencesRank -= 1;
			}

			_dbContext.Set<InstructorPreferences>().Remove(ipOld); // Like a commit

			_dbContext.SaveChanges();

		}
	}

}
