using CASPAR.ApplicationCore.Models;
using CASPAR.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CASPAR.DataAccess
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}


		public void Initialize()
		{
			_db.Database.EnsureCreated();

			//migrations if they are not applied
			try
			{
				if (_db.Database.GetPendingMigrations().Any())
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception)
			{

			}

			if (_db.SemesterSchedules.Any())
			{
				return; //DB has been seeded
			}

			_roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(StaticDetails.Secretary)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(StaticDetails.Student)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(StaticDetails.DC)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(StaticDetails.Instructor)).GetAwaiter().GetResult();
			_roleManager.CreateAsync(new IdentityRole(StaticDetails.PC)).GetAwaiter().GetResult();

			// 1 Admins
			CreateSuperAdmin();
			CreateAdmins();

			// 2 Instructors
			CreateInstructors();
			CreateSecretary();

            // 3 Tickets
            CreateTickets();

			// 4 Program
			CreateProgram();

			// 5 Instructor Program
			CreateInstructorPorgram();

			// 6 Day of Week
			CreateDaysOfTheWeek();

			// 7 Modality
			CreateModality();

			// 8 Semester
			MakeSemester();

			// 9 Release Time
			CreateReleaseTimes();

		

			// 10 Instructor Load
			CreateInstructorLoads();

			// 11 Time of Day
			MakeTimeOfDay();

			// 12 Major
			CreateMajors();

			// 13 Student
			CreateStudentData();

			// 14 Campus
			MakeCampus();

			// 15 Building
			CreateBuildings();

			// 16 Class Room
			CreateClassrooms();

			// 17 Class Room Details
			CreateClassroomDetails();

			// 18 Class Room Configuration
			CreateClassroomConfiguration();

			// 19 Time Slot
			CreateTimeSlot();

			// 20 Who Pays
			CreateWhoPays();

			// 21 Pay Model
			CreatePayModel();

			// 22 Status
			CreateStatus();

			// 23 Course
			InitializeCourse();

			// TEMP Template
			InitializeTemplate();

			// 24 Semester Template
			InitializeSemesterTemplate();

			// 25 Semster Schedule
			InitializeSemesterSchedules();

			// 26 Course Pre Req
			InitializeCoursePrereq();

			// 27 Student Preference
			CreateStudentPreferences();

			// 28 Instructor Preference
			CreateInstructorPreferences();

			//No longer needed
			//// 29 Preference Details
			//CreatePreferenceDetails();

			// 30 Course Section
			CreateCourseSections();

			_db.SaveChanges();
		}

		private void CreateSuperAdmin()
		{
			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "admin@admin.com",
				Email = "admin@admin.com",
				FirstName = "SuperAdmin",
				LastName = "Test",
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "8015550001",
			}, "Temp1234$").GetAwaiter().GetResult();

			_userManager.CreateAsync(new IdentityUser
			{
				Email = "admin@admin.com"
			}, "Temp1234$").GetAwaiter().GetResult();

			ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@admin.com");

			_userManager.AddToRoleAsync(user, StaticDetails.Admin).GetAwaiter().GetResult();
			_db.SaveChanges();
		}

		/// <summary>
		/// Create all the admin users 
		/// </summary>
		private void CreateAdmins()
		{
			// Create User 1
			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "rfry@weber.edu",
				Email = "rfry@weber.edu",
				FirstName = "Richard",
				LastName = "Fry",
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "8015550002",
			}, "Temp1234$").GetAwaiter().GetResult();

			ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "rfry@weber.edu");

			_userManager.AddToRoleAsync(user, StaticDetails.Admin).GetAwaiter().GetResult();

			// Create User 2
			_userManager.CreateAsync(new ApplicationUser
			{
				UserName = "adamjordan@weber.edu",
				Email = "adamjordan@weber.edu",
				FirstName = "Adam",
				LastName = "Jordan",
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "8015550003",
			}, "Temp1234$").GetAwaiter().GetResult();

			ApplicationUser user2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "adamjordan@weber.edu");

			_userManager.AddToRoleAsync(user2, StaticDetails.Admin).GetAwaiter().GetResult();
			_userManager.AddToRoleAsync(user2, StaticDetails.PC).GetAwaiter().GetResult();

			_db.SaveChanges();
		}


		private void CreateInstructors()
		{
			// Create User 1
			_userManager.CreateAsync(new Instructor
			{
				UserName = "hugocowan@weber.edu",
				Email = "hugocowan@weber.edu",
				FirstName = "Hugo",
				LastName = "Cowan",
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "8015550004",
				InstructorType = SDInstructorType.Instructor
			}, "Temp1234$").GetAwaiter().GetResult();

			ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
			_userManager.AddToRoleAsync(user, StaticDetails.Instructor).GetAwaiter().GetResult();

			// Create User 2
			_userManager.CreateAsync(new Instructor
			{
				UserName = "andrewpeterson@weber.edu",
				Email = "andrewpeterson@weber.edu",
				FirstName = "Andrew",
				LastName = "Peterson",
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "8015550005",
				InstructorType = SDInstructorType.Assistant,
				InstructorNotes = "He is a great guy."
			}, "Temp1234$").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");
			_userManager.AddToRoleAsync(user, StaticDetails.Instructor).GetAwaiter().GetResult();

			// Create User 3
			_userManager.CreateAsync(new Instructor
			{
				UserName = "bradelyvalle@weber.edu",
				Email = "bradelyvalle@weber.edu",
				FirstName = "Bradley",
				LastName = "Valle",
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "8015550006",
				InstructorType = SDInstructorType.Professor,
				InstructorNotes = "He seems to dislike this Fry person."
			}, "Temp1234$").GetAwaiter().GetResult();

			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "bradelyvalle@weber.edu");
			_userManager.AddToRoleAsync(user, StaticDetails.Instructor).GetAwaiter().GetResult();


			_db.SaveChanges();
		}

        private void CreateSecretary()
        {
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "martinhorn@weber.edu",
                Email = "martinhorn@weber.edu",
                FirstName = "Martin",
                LastName = "Horn",
                WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
                PhoneNumber = "8015550007",
            }, "Temp1234$").GetAwaiter().GetResult();

            _userManager.CreateAsync(new IdentityUser
            {
                Email = "martinhorn@weber.edu"
            }, "Temp1234$").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "martinhorn@weber.edu");

            _userManager.AddToRoleAsync(user, StaticDetails.Secretary).GetAwaiter().GetResult();
            _db.SaveChanges();
        }

        private void CreateInstructorPreferences()
		{
			// Give hugocowan 2 preferences and andrewpeterson 1 for the first semester
			Instructor instructor = _db.Instructors.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
			Instructor instructor2 = _db.Instructors.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");

			var instructorPreferences = new List<InstructorPreferences>
				{
					new InstructorPreferences{
						WNumber = instructor.Id,
						PreferencesRank = 1,
						CourseID = 1,
						SemesterID = 1,
						ModalityID = 1,
						TimeOfDayID = 2,
						DayOfWeekID = 2,
						CampusID = 1,
					},
					new InstructorPreferences{
						WNumber = instructor.Id,
						PreferencesRank = 2,
						CourseID = 2,
						SemesterID = 1,
						ModalityID = 2,
					},
					new InstructorPreferences{
						WNumber = instructor.Id,
						PreferencesRank = 3,
						CourseID = 3,
						SemesterID = 1,
						ModalityID = 1,
						TimeOfDayID = 1,
						DayOfWeekID = 1,
						CampusID = 1,
					},
					new InstructorPreferences{
						WNumber = instructor.Id,
						PreferencesRank = 4,
						CourseID = 4,
						SemesterID = 1,
						ModalityID = 2,
					},
					new InstructorPreferences{
						WNumber = instructor2.Id,
						PreferencesRank = 1,
						CourseID = 1,
						SemesterID = 1,
						ModalityID = 1,
						TimeOfDayID = 1,
						DayOfWeekID = 1,
						CampusID = 1,
					},
					// Create another for the next semester
					new InstructorPreferences{
						WNumber = instructor.Id,
						PreferencesRank = 1,
						CourseID = 4,
						SemesterID = 2,
						ModalityID = 3,
						TimeOfDayID = 2,
						DayOfWeekID = 2,
					},
				};

			foreach (var i in instructorPreferences)
			{
				_db.InstructorPreferences.Add(i);
			}

			_db.SaveChanges();
		}

		private void CreateInstructorLoads()
		{
			// Give hugocowan 3 loads and andrewpeterson 1
			Instructor instructor = _db.Instructors.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
			Instructor instructor2 = _db.Instructors.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");
			var instructorLoad = new List<InstructorLoad>
				{
					new InstructorLoad{
						WNumber = instructor.Id,
						Load = 4,
						SemesterID = 1
					},
					new InstructorLoad{
						WNumber = instructor.Id,
						Load = 4,
						SemesterID = 1
					},
					new InstructorLoad{
						WNumber = instructor.Id,
						Load = 3,
						SemesterID = 1
					},
					new InstructorLoad{
						WNumber = instructor2.Id,
						Load = 3,
						SemesterID = 1
					}
				};

			foreach (var i in instructorLoad)
			{
				_db.InstructorLoads.Add(i);
			}

			_db.SaveChanges();
		}

		private void CreatePreferenceDetails()
		{

			//Instructor instructor = _db.Instructors.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
			//Instructor instructor2 = _db.Instructors.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");

			//var Instructors = new List<PreferenceDetails>
			//	{
			//		new PreferenceDetails{
			//			ModalityID = 1,
			//			DayOfWeekID = 1,
			//			TimeOfDayID = 1,
			//			CampusID = 1,
			//		},
			//		new PreferenceDetails{
			//			ModalityID = 2
			//		},
			//		new PreferenceDetails{
			//			ModalityID = 2,
			//			DayOfWeekID = 2,
			//			TimeOfDayID = 2,
			//			CampusID = 2,
			//		},
			//		new PreferenceDetails
			//		{
			//			ModalityID = 2,
			//			DayOfWeekID = 2,
			//			TimeOfDayID = 2,
			//			CampusID = 2,
			//		}
			//	};

			//foreach (var i in Instructors)
			//{
			//	_db.PreferenceDetails.Add(i);
			//}
			//_db.SaveChanges();

			// Can add student preferences down here
		}

		private void CreateTickets()
		{
			Instructor instructor = _db.Instructors.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
			Instructor instructor2 = _db.Instructors.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");

			var tickets = new List<Ticket>
				{
					new Ticket{
						WNumber = instructor.Id, // hugocowan first 
						TicketType = SDTicketType.Error,
						TicketDescription = "A ticket with a description."
					},
					new Ticket{
						WNumber = instructor.Id, // hugocowan second 
						TicketType = SDTicketType.Error,

					},
					new Ticket{
						WNumber = instructor2.Id, // andrewpeterson first 
						TicketType = SDTicketType.TimeOff,
						TicketDescription = "Hello, I would like time off for the Holiday. Thank you"
					},
				};

			foreach (var i in tickets)
			{
				_db.Tickets.Add(i);
			}
			_db.SaveChanges();
		}

		public void CreateReleaseTimes()
		{
			Instructor instructor = _db.Instructors.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
			Instructor instructor2 = _db.Instructors.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");


			var releaseTimes = new List<ReleaseTime>
				{
					new ReleaseTime{
						WNumber = instructor.Id, // hugocowan first 
						SemesterID = 2,
						ReleaseTimeType = SDReleaseTimeType.Sick,
						ReleaseTimeNotes = "How did he know?",
						ReleaseTimeAmount = 4  
					},
					new ReleaseTime{
						WNumber = instructor.Id, // hugocowan second 
						SemesterID = 2,
						ReleaseTimeType = SDReleaseTimeType.Vaccation,
						ReleaseTimeNotes = "Sound fun.",
						ReleaseTimeAmount = 3
					},
					new ReleaseTime{
						WNumber = instructor2.Id, // andrewpeterson first 
						SemesterID = 2,
						ReleaseTimeType = SDReleaseTimeType.Vaccation,
						ReleaseTimeNotes = "Why must he go too?",
						ReleaseTimeAmount = 3
					},
				};

			foreach (var i in releaseTimes)
			{
				_db.ReleaseTimes.Add(i);
			}
			_db.SaveChanges();
		}

		private void CreateBuildings()
		{
			int ogdenCampusId = _db.Campus.FirstOrDefault(u => u.CampusName == "Weber State Ogden University")?.Id ?? 1;
			int davisCampusId = _db.Campus.FirstOrDefault(u => u.CampusName == "Weber State Davis University")?.Id ?? 1;

			var buildings = new List<Building>
			{
				new Building { CampusID = ogdenCampusId, BuildingName = "Noorda High Bay" },
				new Building { CampusID = ogdenCampusId, BuildingName = "Tracy Hall Science Center" },
				new Building { CampusID = davisCampusId, BuildingName = "Stewart Center" }
			};

			_db.Building.AddRange(buildings);
			_db.SaveChanges();
		}

		private void CreateClassrooms()
		{
			int noordaBuildingId = _db.Building.FirstOrDefault(u => u.BuildingName == "Noorda High Bay")?.BuildingID ?? 1;
			int tracyBuildingId = _db.Building.FirstOrDefault(u => u.BuildingName == "Tracy Hall Science Center")?.BuildingID ?? 1;
			int stewartBuildingId = _db.Building.FirstOrDefault(u => u.BuildingName == "Stewart Center")?.BuildingID ?? 1;

			var classrooms = new List<Classroom>
			{
				new Classroom { BuildingID = noordaBuildingId, RoomNum = 312, Capacity = 15 },
				new Classroom { BuildingID = tracyBuildingId, RoomNum = 101, Capacity = 25 },
				new Classroom { BuildingID = stewartBuildingId, RoomNum = 113, Capacity = 30 }
			};

			_db.Classroom.AddRange(classrooms);
			_db.SaveChanges();
		}

		private void CreateClassroomDetails()
		{
			/*Classroom ClassroomOne = _db.Classroom.FirstOrDefault(u => u.RoomNum == 312);
			Classroom ClassroomTwo = _db.Classroom.FirstOrDefault(u => u.RoomNum == 101);
			Classroom ClassroomThree = _db.Classroom.FirstOrDefault(u => u.RoomNum == 113);*/

			var classroomDetails = new List<ClassroomDetail>
			{
				//OgdenCampus.Id/DavisCampus.Id When Campus is implemened
				new ClassroomDetail { ClassroomDetails = "Id 1, Classroom details goes here", ClassroomDetailDescrip = "Id 1, Classroom Description goes here"},
				new ClassroomDetail { ClassroomDetails = "Id 2, Classroom details goes here", ClassroomDetailDescrip = "Id 2, Classroom Description goes here"},
				new ClassroomDetail { ClassroomDetails = "Id 3, Classroom details goes here", ClassroomDetailDescrip = "Id 3, Classroom Description goes here"}
			};

			foreach (var c in classroomDetails)
			{
				_db.ClassroomDetail.Add(c);
			}
			_db.SaveChanges();
		}

		private void CreateClassroomConfiguration()
		{
			int ClassroomOne = _db.Classroom.FirstOrDefault(u => u.RoomNum == 312)?.ClassRoomID ?? 1;
			int ClassroomTwo = _db.Classroom.FirstOrDefault(u => u.RoomNum == 101)?.ClassRoomID ?? 1;
			int ClassroomThree = _db.Classroom.FirstOrDefault(u => u.RoomNum == 113)?.ClassRoomID ?? 1;

			var classroomConfiguration = new List<ClassroomConfiguration>
			{
				//OgdenCampus.Id/DavisCampus.Id When Campus is implemened
				new ClassroomConfiguration { ClassRoomID = ClassroomOne, ClassroomDetailID = 1},
				new ClassroomConfiguration { ClassRoomID = ClassroomOne, ClassroomDetailID = 1},
				new ClassroomConfiguration { ClassRoomID = ClassroomOne, ClassroomDetailID = 1}
			};

			foreach (var c in classroomConfiguration)
			{
				_db.ClassroomConfiguration.Add(c);
			}
			_db.SaveChanges();
		}

		private void CreateCourseSections()
		{
			//For use when all models are implemented
			//SemesterSchedule semesterSchedule = _db.SemesterSchedule.FirstOrDefault(u => u.SemesterScheduleID == 1);
			//Course course = _db.Course.FirstOrDefault(u => u.CourseID == 1);
			//Instructor instructor = _db.Instructor.FirstOrDefault(u => u.WNumber == 1);
			//Modality modality = _db.Modality.FirstOrDefault(u => u.ModalityID == 1);
			//Status status = _db.Status.FirstOrDefault(u => u.StatusID == 1);
			//TimeSlot timeSlot = _db.TimeSlot.FirstOrDefault(u => u.TimeSlotID == 1);
			//DayOfWeek dayOfWeek = _db.DayOfWeek.FirstOrDefault(u => u.DayOfWeekID == 1);
			//PayModel payModel = _db.PayModel.FirstOrDefault(u => u.PayModelID == 1);
			//WhoPays whoPays = _db.WhoPays.FirstOrDefault(u => u.WhoPaysID == 1);

			int noordaBuildingId = _db.Building.FirstOrDefault(u => u.BuildingName == "Noorda High Bay")?.BuildingID ?? 1;
			int tracyBuildingId = _db.Building.FirstOrDefault(u => u.BuildingName == "Tracy Hall Science Center")?.BuildingID ?? 1;
			int stewartBuildingId = _db.Building.FirstOrDefault(u => u.BuildingName == "Stewart Center")?.BuildingID ?? 1;

			int foundationsCourse = _db.Courses.FirstOrDefault(u => u.CourseName == "Foundations of Computing")?.CourseID ?? 1;
			int dataCourse = _db.Courses.FirstOrDefault(u => u.CourseName == "Data Structures & Algorithms")?.CourseID ?? 1;
			int dataBaseCourse = _db.Courses.FirstOrDefault(u => u.CourseName == "Intro to Database Design & SQL")?.CourseID ?? 1;

			string instructorOne = _db.Instructors.FirstOrDefault(u => u.Email == "hugocowan@weber.edu")?.Id ?? "01234567";
			string instructorTwo = _db.Instructors.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu")?.Id ?? "01234567";
			string instructorThree = _db.Instructors.FirstOrDefault(u => u.Email == "bradelyvalle@weber.edu")?.Id ?? "01234567";

			//int modalityOne = _db.Modality.FirstOrDefault(u => u.Id == )?.CourseID ?? 1;

			//int statusOne = _db.statuses.FirstOrDefault(u => u.CourseName == "Foundations of Computing")?.CourseID ?? 1;

			//int timeSlotOne = _db.Timeslot.FirstOrDefault(u => u.CourseName == "Foundations of Computing")?.CourseID ?? 1;

			//int payModelOne = _db.payModels.FirstOrDefault(u => u.CourseName == "Foundations of Computing")?.CourseID ?? 1;

			Classroom noordaClassroom = _db.Classroom.FirstOrDefault(u => u.RoomNum == 312 && u.BuildingID == noordaBuildingId);
			Classroom tracyClassroom = _db.Classroom.FirstOrDefault(u => u.RoomNum == 101 && u.BuildingID == tracyBuildingId);
			Classroom stewartClassroom = _db.Classroom.FirstOrDefault(u => u.RoomNum == 113 && u.BuildingID == stewartBuildingId);

			var courseSections = new List<CourseSection>
			{
				new CourseSection
				{
					SemesterScheduleID = 1,
					CourseID = foundationsCourse,
					WNumber = instructorOne,
					ModalityID = 1,
					StatusID = 1,
					ClassRoomID = noordaClassroom?.ClassRoomID ?? 1,
					TimeSlotID = 1,
					DayOfWeekID = 1,
					PartOfTerm = "Part of term goes here",
					FirstDayEnrollment = 1,
					CRN = 1,
					MaxEnrollment = noordaClassroom?.Capacity ?? 15,
					SectionNotes = "Section Notes go here",
					PayModelID = 1,
					WhoPaysID = 1
				},
				new CourseSection
				{
					SemesterScheduleID = 2,
					CourseID = dataCourse,
					WNumber = instructorTwo,
					ModalityID = 2,
					StatusID = 2,
					ClassRoomID = tracyClassroom?.ClassRoomID ?? 1,
					TimeSlotID = 2,
					DayOfWeekID = 2,
					PartOfTerm = "Part of term goes here",
					FirstDayEnrollment = 2,
					CRN = 2,
					MaxEnrollment = tracyClassroom?.Capacity ?? 15,
					SectionNotes = "Section Notes go here",
					PayModelID = 2,
					WhoPaysID = 2
				},
				new CourseSection
				{
					SemesterScheduleID = 3,
					CourseID = dataBaseCourse,
					WNumber = instructorThree,
					ModalityID = 3,
					StatusID = 2,
					ClassRoomID = stewartClassroom?.ClassRoomID ?? 1,
					TimeSlotID = 3,
					DayOfWeekID = 3,
					PartOfTerm = "Part of term goes here",
					FirstDayEnrollment = 3,
					CRN = 3,
					MaxEnrollment = stewartClassroom?.Capacity ?? 15,
					SectionNotes = "Section Notes go here",
					PayModelID = 2,
					WhoPaysID = 2
				}
			};

			foreach (var c in courseSections)
			{
				_db.CourseSection.Add(c);
			}
			_db.SaveChanges();
		}

		private void CreateDaysOfTheWeek()
		{
			var dayOfTheWeek = new List<DayOfTheWeek>
			{
				//OgdenCampus.Id/DavisCampus.Id When Campus is implemened
				new DayOfTheWeek { DayOfWeek = "Monday"},
				new DayOfTheWeek { DayOfWeek = "Tuesday"},
				new DayOfTheWeek { DayOfWeek = "Wednesday"},
				new DayOfTheWeek { DayOfWeek = "Thursday"},
				new DayOfTheWeek { DayOfWeek = "Friday"},
				new DayOfTheWeek { DayOfWeek = "Saturday"},
				new DayOfTheWeek { DayOfWeek = "Sunday"}
			};

			foreach (var c in dayOfTheWeek)
			{
				_db.DayOfTheWeek.Add(c);
			}
			_db.SaveChanges();
		}

		private void MakeTimeOfDay()
		{
			_db.TimeOfDays.Add(new TimeOfDay { Time = "Morning" });
			_db.TimeOfDays.Add(new TimeOfDay { Time = "Afternoon" });
			_db.TimeOfDays.Add(new TimeOfDay { Time = "Evening" });
			_db.SaveChanges();
		}

		private void MakeSemester()
		{
			_db.Semester.Add(new Semester { SemesterName = "Fall", SemesterYear = 2023 });
			_db.Semester.Add(new Semester { SemesterName = "Spring", SemesterYear = 2023 });
			_db.Semester.Add(new Semester { SemesterName = "Summer", SemesterYear = 2023 });
			_db.Semester.Add(new Semester { SemesterName = "Fall", SemesterYear = 2024 });
			_db.Semester.Add(new Semester { SemesterName = "Spring", SemesterYear = 2024 });
			_db.Semester.Add(new Semester { SemesterName = "Summer", SemesterYear = 2024 });
			_db.Semester.Add(new Semester { SemesterName = "Fall", SemesterYear = 2025 });
			_db.Semester.Add(new Semester { SemesterName = "Spring", SemesterYear = 2025 });
			_db.Semester.Add(new Semester { SemesterName = "Summer", SemesterYear = 2025 });
			_db.SaveChanges();
		}

		private void MakeCampus()
		{
			_db.Campus.Add(new Campus { CampusName = "Weber State Ogden University", Address1 = "3910 S W Campus Dr", City = "Ogden", State = "UT", Zip = "84408", Country = "America" });
			_db.Campus.Add(new Campus { CampusName = "Weber State Davis University", Address1 = "2750 University Park Blvd", City = " Layton", State = "UT", Zip = "84041", Country = "America" });
			_db.SaveChanges();
		}

		private void InitializeCourse()
		{
			var Courses = new List<Course>
			{
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "1030",
					CourseName = "Foundations of Computing",
					CreditHours = 4,
					CourseDescription = "Computers are an essential part of every occupation. Having a basic understanding of computers will help students become more confident users. This course is taught at an introductory level and presents a broad overview of topics in computing such as personal digital security, ethical behaviors in education and business, how computers work and communicate with one another, how data is stored and used in a computer, and how to create a website and write a computer program."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "2420",
					CourseName = "Data Structures & Algorithms",
					CreditHours = 4,
					CourseDescription = "General principles of common data structures and design of efficient algorithms. Topics include: arrays, linked-lists, stacks, queues, trees, graphs, tables, storage and retrieval structures, searching, sorting, hashing, and algorithmic analysis. Emphasis will be on abstraction, efficiency, re-usable code, and object-oriented implementation."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "2550",
					CourseName = "Intro to Database Design & SQL",
					CreditHours = 4,
					CourseDescription = "This course is an introduction to databases, specifically focusing on the relational database model, database design and modeling and the structured query language (SQL). Students will become proficient at formulating data query requests using SQL and will also gain experience in database normalization and entity-relationship modeling."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "2705",
					CourseName = "Network Fundamentals/Design",
					CreditHours = 4,
					CourseDescription = "Provide an understanding of the basic networking terminology. This will cover the theory of networking, types of network protocols, and wide and local area networks. The student should have a good understanding of network terminology at the completion of the course."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "3100",
					CourseName = "Operating Systems",
					CreditHours = 4,
					CourseDescription = "An overview of computer operating system from the programmer’s point of view. Input-output hardware, interrupt handling, properties of external storage devices, associative memories and virtual address translation techniques, optimizing programs for performance, concurrent programming with threads, and network programming."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "4800",
					CourseName = "Individual Projects & Research",
					CreditHours = 1,
					CourseDescription = "The purpose of this course is to permit Computer Science majors to develop an individual project, program, system, or research paper, with coordination and approval of a faculty mentor. The final grade and amount of credit awarded will be determined by the department, depending on the complexity of the upper division work performed. May be repeated 3 times up to 4 credit hours. Note: Only 4 credit hours of CS 4800 or CS 4850 or CS 4890 can apply to a CS degree as an elective course, and only a maximum of 6 hours of CS 4800, CS 4850, and CS 4890 may be taken to satisfy missing credits or to achieve full time academic status."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "4890",
					CourseName = "INT Cooperative Work Exp",
					CreditHours = 1,
					CourseDescription = "The purpose of this course is to permit Computer Science majors who are currently working in a computer related job or internship to receive academic credit for their work, with coordination and approval of a faculty mentor and their supervisor. The amount of upper division credit awarded will be determined by the department, depending on the nature and quantity of work performed. May be repeated 3 times up to 4 credit hours. Note: Only 4 credit hours of CS 4800 or CS 4850 or CS 4890 can apply to a CS degree as an elective course, and only a maximum of 6 hours of CS 4800, CS 4850, and CS 4890 may be taken to satisfy missing credits or to achieve full time academic status."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "6011",
					CourseName = "Thesis Research",
					CreditHours = 2,
					CourseDescription = "Students are required to complete original computer science research resulting in a thesis. Students must demonstrate proficiency in research, design, analysis, project planning, implementation, testing, presentation and documentation. Students receive T (temporary) grades until their final design review, after which these grades are changed retroactively. Students must be enrolled in CS 6011 at the time of their final thesis defense."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "1410",
					CourseName = "Object-Oriented Programming",
					CreditHours = 4,
					CourseDescription = "An introduction to the C++ language. Topics will include data types, control structures, functions, pointers, arrays, I/O streams, classes, objects, encapsulation, overloading, inheritance and use of these concepts in problem solving."
				},
				new Course
				{
					ProgramPrefix = SDCourseProgramPrefix.CS,
					CourseNumber = "2350",
					CourseName = "Client Side Web Development\r\n",
					CreditHours = 4,
					CourseDescription = "This course provides an introduction to client-side programming and Web page development. Subjects covered include responsive Web page design and dynamic Web page development. The course will explore various technologies such as HTML5, CSS3, JavaScript client-side programming, and an introduction to a JavaScript framework."
				}
			};

			foreach (var i in Courses)
			{
				_db.Courses.Add(i);
			}

			_db.SaveChanges();

		}

		private void InitializeTemplate()
		{
			// Create Templates 
			var Templates = new List<Template> {
				new Template
				{
					TemplateName = "Fall"
				},
				new Template
				{
					TemplateName = "Spring"
				},
				new Template
				{
					TemplateName = "Summer"
				}
			};

			foreach (var i in Templates)
			{
				_db.Templates.Add(i);
			}

			_db.SaveChanges();
		}

		private void InitializeSemesterTemplate()
		{
			// Create Semester Templates 
			var SemesterTemplates = new List<SemesterTemplate> {
				new SemesterTemplate
				{
					CourseID = 1,
					TemplateID = 1,
					CourseQuantity = 1
				},
				new SemesterTemplate
				{
					CourseID = 2,
					TemplateID = 1,
					CourseQuantity = 2
				},
				new SemesterTemplate
				{
					CourseID = 4,
					TemplateID = 1,
					CourseQuantity = 3
				},
				new SemesterTemplate
				{
					CourseID = 3,
					TemplateID = 2,
					CourseQuantity = 4
				},
				new SemesterTemplate
				{
					CourseID = 4,
					TemplateID = 2,
					CourseQuantity = 5
				},
				new SemesterTemplate
				{
					CourseID = 3,
					TemplateID = 3,
					CourseQuantity = 6
				},
				new SemesterTemplate
				{
					CourseID = 4,
					TemplateID = 3,
					CourseQuantity = 7
				},
				new SemesterTemplate
				{
					CourseID = 1,
					TemplateID = 3,
					CourseQuantity = 8
				}
			};

			foreach (var i in SemesterTemplates)
			{
				_db.SemesterTemplates.Add(i);
			}

			_db.SaveChanges();

		}


		private void InitializeCoursePrereq()
		{
			var CoursePrereqs = new List<CoursePrereq>
			{
				new CoursePrereq
				{
					CourseID = 1,
					PrereqNotes = "CS 1410 with a minimum grade of C\r\n(or CS 1400 with a minimum grade of C)\r\n(and NET 3200 with a minimum grade of C\r\nor NET 2210 with a minimum grade of C)"
				},
				new CoursePrereq
				{
					CourseID = 3,
					PrereqNotes = "CS 1400 with a minimum grade of C\r\nand CS 1030 with a minimum grade of C"
				},
				new CoursePrereq
				{
					CourseID = 5,
					PrereqNotes = "(CS 1400 with a minimum grade of C\r\nor ECE 1400 with a minimum grade of C\r\nor CS 2250 with a minimum grade of C)\r\n(and ENGL 1010 with a minimum grade of C\r\nor ENGL 2010 with a minimum grade of C)"
				},
				new CoursePrereq
				{
					CourseID = 6,
					PrereqNotes = "CS 1400 with a minimum grade of C\r\n"
				},
				new CoursePrereq
				{
					CourseID = 7,
					PrereqNotes = "CS 1410 with a minimum grade of C\r\nand MATH 1050 with a minimum grade of C\r\nor MATH 1080 with a minimum grade of C\r\nor MATH 1210 with a minimum grade of C"
				},
				new CoursePrereq
				{
					CourseID = 8,
					PrereqNotes = "CS 1410 with a minimum grade of C\r\n"
				},
				new CoursePrereq
				{
					CourseID = 10,
					PrereqNotes = "(CS 1030 with a minimum grade of C\r\nor WEB 1030 with a minimum grade of C\r\nor NET 1030 with a minimum grade of C)\r\nand CS 1400 with a minimum grade of C"
				}
			};

			foreach (var p in CoursePrereqs)
			{
				_db.CoursePrereqs.Add(p);
			}

			_db.SaveChanges();
		}

		private void InitializeSemesterSchedules()
		{
			var SemesterSchedules = new List<SemesterSchedule>
			{
				new SemesterSchedule
				{
					SemesterTemplateID = 1,
					StatusID = 2,
					ScheduleName = "Peter",
					OpenEnrollmentDate = DateTime.Now,
					CloseEnrollmentDate = DateTime.Now,
					SemesterID = 1,
				},
				new SemesterSchedule
				{
					SemesterTemplateID = 2,
					StatusID = 1,
					ScheduleName = "Carl",
					OpenEnrollmentDate = DateTime.Now,
					CloseEnrollmentDate = DateTime.Now,
					SemesterID = 2,
				},
				new SemesterSchedule
				{
					SemesterTemplateID = 5,
					StatusID = 2,
					ScheduleName = "Susan",
					OpenEnrollmentDate = DateTime.Now,
					CloseEnrollmentDate = DateTime.Now,
					SemesterID = 2,
				}
			};

			foreach (var s in SemesterSchedules)
			{
				_db.SemesterSchedules.Add(s);
			}

			_db.SaveChanges();
		}

		private void CreateMajors()
		{
			_db.Major.Add(new Major { MajorName = "Computer Science" });
			_db.Major.Add(new Major { MajorName = "Computer Science Teaching" });
			_db.Major.Add(new Major { MajorName = "Physics" });
			_db.Major.Add(new Major { MajorName = "Math" });
			_db.Major.Add(new Major { MajorName = "English" });
			_db.Major.Add(new Major { MajorName = "History" });
			_db.Major.Add(new Major { MajorName = "Art" });
			_db.Major.Add(new Major { MajorName = "Biology" });
			_db.SaveChanges();
		}

		private void CreateStudentData()
		{
			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Junior",
				Email = "sirmeowington@mail.weber.com",
				UserName = "sirmeowington@mail.weber.com",
				FirstName = "Sir",
				LastName = "Meowington",
				GraduationYear = 2025,
				MajorId = 1,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "1234567890"
			}, "Temp1234$").GetAwaiter().GetResult();

			ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "sirmeowington@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Senior",
				Email = "whiskersalot@mail.weber.com",
				UserName = "whiskersalot@mail.weber.com",
				FirstName = "Whiskers",
				LastName = "Alot",
				EmailConfirmed = true,
				GraduationYear = 2026,
				MajorId = 2,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "1234467890"
			}, "*Testing213*").GetAwaiter().GetResult();
			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "whiskersalot@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Freshman",
				Email = "nibblestor@mail.weber.com",
				UserName = "nibblestor@mail.weber.com",
				FirstName = "Nibbles",
				LastName = "Tor",
				EmailConfirmed = true,
				GraduationYear = 2024,
				MajorId = 3,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "1434567890"
			}, "*Testing12344*").GetAwaiter().GetResult();
			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "nibblestor@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Sophmore",
				Email = "knightmeowers@mail.weber.com",
				UserName = "knightmeowers@mail.weber.com",
				FirstName = "Knight",
				LastName = "Meowers",
				EmailConfirmed = true,
				GraduationYear = 2024,
				MajorId = 4,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "1237767890"
			}, "*Testing777*").GetAwaiter().GetResult();
			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "knightmeowers@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Senior",
				Email = "arthurmeowsalot@mail.weber.com",
				UserName = "arthurmeowsalot@mail.weber.com",
				FirstName = "Arthur",
				LastName = "Meowsalot",
				EmailConfirmed = true,
				GraduationYear = 2025,
				MajorId = 6,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "4434567890"
			}, "*Testing44444*").GetAwaiter().GetResult();
			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "arthurmeowsalot@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Junior",
				Email = "codingcat@mail.weber.com",
				UserName = "codingcat@mail.weber.com",
				FirstName = "Coding",
				LastName = "Cat",
				EmailConfirmed = true,
				GraduationYear = 2026,
				MajorId = 1,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "1237777890"
			}, "*Testing7777777*").GetAwaiter().GetResult();
			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "codingcat@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_userManager.CreateAsync(new Student
			{
				ClassStanding = "Junior",
				Email = "logangood@mail.weber.com",
				UserName = "logangood@mail.weber.com",
				FirstName = "Logan",
				LastName = "Good",
				EmailConfirmed = true,
				GraduationYear = 2026,
				MajorId = 1,
				WNumber = SDSchoolId.GenerateNewSchoolIdNumber(),
				PhoneNumber = "1237777891"
			}, "Temp1234$").GetAwaiter().GetResult();
			user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "logangood@mail.weber.com");
			_userManager.AddToRoleAsync(user, StaticDetails.Student).GetAwaiter().GetResult();

			_db.SaveChanges();
		}

		private void CreateStudentPreferences()
		{
			Student student1 = _db.Students.FirstOrDefault(s => s.Email == "codingcat@mail.weber.com");
			Student student2 = _db.Students.FirstOrDefault(s => s.Email == "logangood@mail.weber.com");


			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student2.Id,
				CourseID = 1,
				SemesterID = 1,
				ModalityID = 1,
				TimeOfDayID = 1,
				CampusID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student2.Id,
				CourseID = 2,
				SemesterID = 1,
				ModalityID = 1,
				TimeOfDayID = 2,
				CampusID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student2.Id,
				CourseID = 3,
				SemesterID = 1,
				ModalityID = 1,
				TimeOfDayID = 3,
				CampusID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student2.Id,
				CourseID = 4,
				SemesterID = 1,
				ModalityID = 2,
			});


			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student2.Id,
				CourseID = 5,
				SemesterID = 2,
				ModalityID = 2,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student2.Id,
				CourseID = 6,
				SemesterID = 2,
				ModalityID = 1,
				TimeOfDayID = 1,
				CampusID = 1,
			});

			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student1.Id,
				CourseID = 1,
				SemesterID = 1,
				ModalityID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student1.Id,
				CourseID = 1,
				SemesterID = 3,
				ModalityID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student1.Id,
				CourseID = 2,
				SemesterID = 2,
				ModalityID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student1.Id,
				CourseID = 1,
				SemesterID = 2,
				ModalityID = 1,
			});
			_db.StudentPreferences.Add(new StudentPreferences
			{
				WNumber = student1.Id,
				CourseID = 2,
				SemesterID = 1,
				ModalityID = 1,
			});
			_db.SaveChanges();
		}

		private void CreateModality()
		{
			_db.Modality.Add(new Modality { ModalityType = "FTF", ModalityDescription = "Face to Face" });
			_db.Modality.Add(new Modality { ModalityType = "ONL", ModalityDescription = "Online" });
			_db.Modality.Add(new Modality { ModalityType = "VRT", ModalityDescription = "Virtual" });
			_db.Modality.Add(new Modality { ModalityType = "HYB", ModalityDescription = "Hybrid" });
			_db.SaveChanges();
		}

		private void CreateProgram() 
		{
			_db.programs.Add(new Program { ProgramName = "CS" });
			_db.programs.Add(new Program { ProgramName = "NET" });
			_db.programs.Add(new Program { ProgramName = "WEB" });
			_db.SaveChanges();
		}

		private void CreateWhoPays()
		{
			_db.WhoPays.Add(new WhoPays { WhoPaysType = "EAST" });
			_db.WhoPays.Add(new WhoPays { WhoPaysType = "Grant" });
			_db.SaveChanges();
		}

		private void CreateInstructorPorgram()
		{
            ApplicationUser user1 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "hugocowan@weber.edu");
            ApplicationUser user2 = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "andrewpeterson@weber.edu");

            _db.InstructorPrograms.Add(new InstructorProgram { WNumber = user1.Id, ProgramID = 1});
			_db.InstructorPrograms.Add(new InstructorProgram { WNumber = user2.Id, ProgramID = 2});
            _db.InstructorPrograms.Add(new InstructorProgram { WNumber = user2.Id, ProgramID = 3});

            _db.SaveChanges();
		}

		private void CreateStatus()
		{
			_db.statuses.Add(new Status { StatusType = "Confirmed", StatusDescription = "This couse is confirmed by Program Coordinator" });
			_db.statuses.Add(new Status { StatusType = "Pending", StatusDescription = "This course is not confirmed still in pending" });
			_db.SaveChanges ();
		}

		private void CreateTimeSlot()
		{
			_db.TimeSlots.Add(new TimeSlot { TimeSlotType = "MW", TimeSlotDescription = "Monday and Wednesday" });
			_db.TimeSlots.Add(new TimeSlot { TimeSlotType = "TT", TimeSlotDescription = "Tuesday and Thursday" });
			_db.TimeSlots.Add(new TimeSlot { TimeSlotType = "MWF", TimeSlotDescription = "Monday, Wednesday and Friday" });
			_db.SaveChanges();
		}

		private void CreatePayModel()
		{
			_db.payModels.Add(new PayModel { PayModelType = "Regular" });
			_db.payModels.Add(new PayModel { PayModelType = "Overload" });
			_db.SaveChanges();
		}
	}
}
