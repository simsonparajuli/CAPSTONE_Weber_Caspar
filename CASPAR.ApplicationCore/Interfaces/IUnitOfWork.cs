using CASPAR.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CASPAR.ApplicationCore.Interfaces
{
	public interface IUnitOfWork
	{

		// ADD other Repositories
		public IPreferencesRepository<InstructorPreferences> Preferences { get; }

		//ADD other Models/Tables here as you create them
		//public IGenericRepository<Manufacturer> Manufacturer { get; }

		public IGenericRepository<ApplicationUser> ApplicationUser { get; }
		public IGenericRepository<Building> Building { get; }
		public IGenericRepository<Classroom> Classroom { get; }
		public IGenericRepository<ClassroomDetail> ClassroomDetail { get; }

		public IGenericRepository<ClassroomConfiguration> ClassroomConfiguration { get; }
		public IGenericRepository<Models.DayOfTheWeek> DAY_OF_WEEK { get; }
		public IGenericRepository<CourseSection> SECTION { get; }


        public IGenericRepository<Instructor> Instructor { get; }

        public IGenericRepository<InstructorPreferences> InstructorPreferences { get; }

		public IGenericRepository<InstructorLoad> InstructorLoad { get; }

		public IGenericRepository<PreferenceDetails> PreferenceDetails { get; }

		public IGenericRepository<Ticket> Tickets { get; }

		public IGenericRepository<ReleaseTime> ReleaseTimes { get; }


		public IGenericRepository<Student> Student { get; }
		public IGenericRepository<TimeOfDay> TimeOfDay { get; }
		public IGenericRepository<Modality> Modality { get; }
		public IGenericRepository<Semester> Semester { get; }
		public IGenericRepository<Campus> Campus { get; }
		public IGenericRepository<StudentPreferences> StudentPreferences { get; }
		public IGenericRepository<Major> Major { get; }


		public IGenericRepository<Program> Program { get; }
		public IGenericRepository<InstructorProgram> InstructorProgram { get; }
		public IGenericRepository<WhoPays> WhoPays { get; }
		public IGenericRepository <Status> Status { get; }
		public IGenericRepository<TimeSlot> TimeSlot { get; }
		public IGenericRepository<PayModel> PayModel { get; }	

		public IGenericRepository<Course> Course { get; }
		public IGenericRepository<CoursePrereq> CoursePrereq { get; }
		public IGenericRepository<SemesterTemplate> SemesterTemplate { get; }
		public IGenericRepository<SemesterSchedule> SemesterSchedule { get; }

		public IGenericRepository<Template> Template { get; }
		//save changes to the data source
		int Commit();

		Task<int> CommitAsync();
	}
}
