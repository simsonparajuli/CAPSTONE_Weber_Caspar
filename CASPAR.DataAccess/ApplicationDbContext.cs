using CASPAR.ApplicationCore.Interfaces;
using CASPAR.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CASPAR.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<InstructorPreferences> InstructorPreferences { get; set; }

		public DbSet<InstructorLoad> InstructorLoads { get; set; }

		public DbSet<PreferenceDetails> PreferenceDetails { get; set; }

		public DbSet<Ticket> Tickets { get; set; }

		public DbSet<ReleaseTime> ReleaseTimes { get; set; }

		//public DbSet<Category> Categories { get; set; }
		public DbSet<Building> Building { get; set; }
		public DbSet<Classroom> Classroom { get; set; }
		public DbSet<ClassroomDetail> ClassroomDetail { get; set; }

		public DbSet<ClassroomConfiguration> ClassroomConfiguration { get; set; }
		public DbSet<DayOfTheWeek> DayOfTheWeek { get; set; }
		public DbSet<CourseSection> CourseSection { get; set; }

		public DbSet<Student> Students { get; set; }
		public DbSet<TimeOfDay> TimeOfDays { get; set; }
		public DbSet<Modality> Modality { get; set; }
		public DbSet<Semester> Semester { get; set; }
		public DbSet<Campus> Campus { get; set; }
		public DbSet<StudentPreferences> StudentPreferences { get; set; }
		public DbSet<Major> Major { get; set; }

		public DbSet<Course> Courses { get; set; }
		public DbSet<SemesterTemplate> SemesterTemplates { get; set; }
		public DbSet<Template> Templates { get; set; }
		public DbSet<CoursePrereq> CoursePrereqs { get; set; }
		public DbSet<SemesterSchedule> SemesterSchedules { get; set; }

		public DbSet<Program> programs { get; set; }
		public DbSet<InstructorProgram> InstructorPrograms { get; set; }
		public DbSet <WhoPays> WhoPays { get; set;}
		public DbSet<Status> statuses { get; set; }
		public DbSet<PayModel> payModels { get; set; }
		public DbSet<TimeSlot> TimeSlots { get; set; }



	}
}