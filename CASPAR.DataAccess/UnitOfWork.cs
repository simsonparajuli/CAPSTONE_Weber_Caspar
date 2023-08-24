using CASPAR.ApplicationCore.Interfaces;
using CASPAR.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;  //dependency injection of Data Source

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// Add other repositories
		public IPreferencesRepository<InstructorPreferences> _Preferences;


		// Add Properties HERE
		public IGenericRepository<ApplicationUser> _ApplicationUser;
		public IGenericRepository<Instructor> _Instructor;
		public IGenericRepository<InstructorPreferences> _InstructorPreferences;
		public IGenericRepository<InstructorLoad> _InstructorLoad;
		public IGenericRepository<PreferenceDetails> _PreferenceDetails;
		public IGenericRepository<Ticket> _Tickets;
		public IGenericRepository<ReleaseTime> _ReleaseTimes;

		public IGenericRepository<Building> _Building;
		public IGenericRepository<Classroom> _Classroom;
		public IGenericRepository<ClassroomDetail> _ClassroomDetail;
		public IGenericRepository<ClassroomConfiguration> _ClassroomConfiguration;
		public IGenericRepository<ApplicationCore.Models.DayOfTheWeek> _DAY_OF_WEEK;
		public IGenericRepository<CourseSection> _SECTION;
		public IGenericRepository<Student> _Student;
		public IGenericRepository<TimeOfDay> _TimeOfDay;
		public IGenericRepository<Modality> _Modality;
		public IGenericRepository<Semester> _Semester;
		public IGenericRepository<Campus> _Campus;
		public IGenericRepository<StudentPreferences> _StudentPreferences;
		public IGenericRepository<Major> _Major;

		public IGenericRepository<Program> _Program;
		public IGenericRepository<InstructorProgram> _InstructorProgram;
		public IGenericRepository<WhoPays> _WhoPays;
		public IGenericRepository<Status> _Status;
		public IGenericRepository<TimeSlot> _TimeSlot;
		public IGenericRepository<PayModel> _PayModel;

		public IGenericRepository<Course> _Course;
		public IGenericRepository<CoursePrereq> _CoursePrereq;
		public IGenericRepository<SemesterTemplate> _SemesterTemplate;
		public IGenericRepository<Template> _Template;
		public IGenericRepository<SemesterSchedule> _SemesterSchedule;

		public IPreferencesRepository<InstructorPreferences> Preferences
		{
			get
			{
				if (_Preferences == null) _Preferences = new PreferencesRepository(_dbContext);
				return _Preferences;
			}
		}


		public IGenericRepository<ApplicationUser> ApplicationUser
		{
			get
			{
				if (_ApplicationUser == null) _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
				return _ApplicationUser;
			}
		}

		public IGenericRepository<Instructor> Instructor
		{
			get
			{
				if (_Instructor == null) _Instructor = new GenericRepository<Instructor>(_dbContext);
				return _Instructor;
			}
		}

		public IGenericRepository<InstructorPreferences> InstructorPreferences
		{
			get
			{
				if (_InstructorPreferences == null) _InstructorPreferences = new GenericRepository<InstructorPreferences>(_dbContext);
				return _InstructorPreferences;
			}
		}

		public IGenericRepository<InstructorLoad> InstructorLoad
		{
			get
			{
				if (_InstructorLoad == null) _InstructorLoad = new GenericRepository<InstructorLoad>(_dbContext);
				return _InstructorLoad;
			}
		}

		public IGenericRepository<PreferenceDetails> PreferenceDetails
		{
			get
			{
				if (_PreferenceDetails == null) _PreferenceDetails = new GenericRepository<PreferenceDetails>(_dbContext);
				return _PreferenceDetails;
			}
		}


		public IGenericRepository<Building> Building
		{
			get
			{
				if (_Building == null) _Building = new GenericRepository<Building>(_dbContext);
				return _Building;
			}
		}
		public IGenericRepository<Classroom> Classroom
		{
			get
			{
				if (_Classroom == null) _Classroom = new GenericRepository<Classroom>(_dbContext);
				return _Classroom;
			}
		}
		public IGenericRepository<ClassroomDetail> ClassroomDetail
		{
			get
			{
				if (_ClassroomDetail == null) _ClassroomDetail = new GenericRepository<ClassroomDetail>(_dbContext);
				return _ClassroomDetail;
			}
		}

		public IGenericRepository<ClassroomConfiguration> ClassroomConfiguration
		{
			get
			{
				if (_ClassroomConfiguration == null) _ClassroomConfiguration = new GenericRepository<ClassroomConfiguration>(_dbContext);
				return _ClassroomConfiguration;
			}
		}
		public IGenericRepository<ApplicationCore.Models.DayOfTheWeek> DAY_OF_WEEK
		{
			get
			{
				if (_DAY_OF_WEEK == null) _DAY_OF_WEEK = new GenericRepository<ApplicationCore.Models.DayOfTheWeek>(_dbContext);
				return _DAY_OF_WEEK;
			}
		}
		public IGenericRepository<CourseSection> SECTION
		{
			get
			{
				if (_SECTION == null) _SECTION = new GenericRepository<CourseSection>(_dbContext);
				return _SECTION;
			}
		}

		public IGenericRepository<TimeOfDay> TimeOfDay
		{
			get
			{
				if (_TimeOfDay == null) _TimeOfDay = new GenericRepository<TimeOfDay>(_dbContext);
				return _TimeOfDay;
			}
		}

		public IGenericRepository<Student> Student
		{
			get
			{
				if (_Student == null) _Student = new GenericRepository<Student>(_dbContext);
				return _Student;
			}
		}

		public IGenericRepository<Modality> Modality
		{
			get
			{
				if (_Modality == null) _Modality = new GenericRepository<Modality>(_dbContext);
				return _Modality;
			}
		}

		public IGenericRepository<Semester> Semester
		{
			get
			{
				if (_Semester == null) _Semester = new GenericRepository<Semester>(_dbContext);
				return _Semester;
			}
		}

		public IGenericRepository<Campus> Campus
		{
			get
			{
				if (_Campus == null) _Campus = new GenericRepository<Campus>(_dbContext);
				return _Campus;
			}
		}

		public IGenericRepository<StudentPreferences> StudentPreferences
		{
			get
			{
				if (_StudentPreferences == null) _StudentPreferences = new GenericRepository<StudentPreferences>(_dbContext);
				return _StudentPreferences;
			}
		}

		public IGenericRepository<Major> Major
		{
			get
			{
				if (_Major == null) _Major = new GenericRepository<Major>(_dbContext);
				return _Major;
			}
		}

		public IGenericRepository<Program> Program
		{
			get
			{
				if (_Program == null) _Program = new GenericRepository<Program>(_dbContext);
				return _Program;
			}
		}

		public IGenericRepository<InstructorProgram> InstructorProgram
		{
			get
			{
				if (_InstructorProgram == null) _InstructorProgram = new GenericRepository<InstructorProgram>(_dbContext);
				return _InstructorProgram;
			}
		}

		public IGenericRepository<WhoPays> WhoPays
		{
			get
			{
				if (_WhoPays == null) _WhoPays = new GenericRepository<WhoPays>(_dbContext);
				return _WhoPays;
			}
		}

		public IGenericRepository<Status> Status
		{
			get
			{
				if (_Status == null) _Status = new GenericRepository<Status>(_dbContext);
				return _Status;
			}
		}

		public IGenericRepository<TimeSlot> TimeSlot
		{
			get
			{
				if (_TimeSlot == null) _TimeSlot = new GenericRepository<TimeSlot>(_dbContext);
				return _TimeSlot;
			}
		}

		public IGenericRepository<PayModel> PayModel
		{
			get
			{
				if (_PayModel == null) _PayModel = new GenericRepository<PayModel>(_dbContext);
				return _PayModel;
			}
		}
		public IGenericRepository<Ticket> Tickets
		{
			get
			{
				if (_Tickets == null) _Tickets = new GenericRepository<Ticket>(_dbContext);
				return _Tickets;
			}
		}
		public IGenericRepository<Course> Course
		{
			get
			{
				if (_Course == null) _Course = new GenericRepository<Course>(_dbContext);
				return _Course;
			}
		}
		public IGenericRepository<CoursePrereq> CoursePrereq
		{
			get
			{
				if (_CoursePrereq == null) _CoursePrereq = new GenericRepository<CoursePrereq>(_dbContext);
				return _CoursePrereq;
			}
		}
		public IGenericRepository<SemesterTemplate> SemesterTemplate
		{
			get
			{
				if (_SemesterTemplate == null) _SemesterTemplate = new GenericRepository<SemesterTemplate>(_dbContext);
				return _SemesterTemplate;
			}
		}
		public IGenericRepository<Template> Template
		{
			get
			{
				if (_Template == null) _Template = new GenericRepository<Template>(_dbContext);
				return _Template;
			}
		}
		public IGenericRepository<SemesterSchedule> SemesterSchedule
		{
			get
			{
				if (_SemesterSchedule == null) _SemesterSchedule = new GenericRepository<SemesterSchedule>(_dbContext);
				return _SemesterSchedule;
			}
		}

		public IGenericRepository<ReleaseTime> ReleaseTimes
		{
			get
			{
				if (_ReleaseTimes == null) _ReleaseTimes = new GenericRepository<ReleaseTime>(_dbContext);
				return _ReleaseTimes;
			}
		}

		public int Commit()
		{
			return _dbContext.SaveChanges();
		}

		public async Task<int> CommitAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		//additional method added for garbage disposal

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
