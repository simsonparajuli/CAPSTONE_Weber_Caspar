using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CASPAR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassroomConfiguration",
                columns: table => new
                {
                    ClassroomConfigID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<int>(type: "int", nullable: false),
                    ClassroomDetailID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomConfiguration", x => x.ClassroomConfigID);
                });

            migrationBuilder.CreateTable(
                name: "ClassroomDetail",
                columns: table => new
                {
                    ClassroomDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassroomDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassroomDetailDescrip = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassroomDetail", x => x.ClassroomDetailID);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrereqs",
                columns: table => new
                {
                    CoursePrereqID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    PrereqNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrereqs", x => x.CoursePrereqID);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramPrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "DayOfTheWeek",
                columns: table => new
                {
                    DayOfTheWeekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfTheWeek", x => x.DayOfTheWeekID);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModalityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModalityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payModels",
                columns: table => new
                {
                    PayModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayModelType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payModels", x => x.PayModelID);
                });

            migrationBuilder.CreateTable(
                name: "programs",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programs", x => x.ProgramID);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemesterYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    TemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateID);
                });

            migrationBuilder.CreateTable(
                name: "TimeOfDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSlotType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSlotDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlotID);
                });

            migrationBuilder.CreateTable(
                name: "WhoPays",
                columns: table => new
                {
                    WhoPaysID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhoPaysType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhoPays", x => x.WhoPaysID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusID = table.Column<int>(type: "int", nullable: false),
                    BuildingName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingID);
                    table.ForeignKey(
                        name: "FK_Building_Campus_CampusID",
                        column: x => x.CampusID,
                        principalTable: "Campus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructorNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassStanding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorId = table.Column<int>(type: "int", nullable: true),
                    GraduationYear = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Major_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Major",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorPrograms",
                columns: table => new
                {
                    InstructorProgramID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorPrograms", x => x.InstructorProgramID);
                    table.ForeignKey(
                        name: "FK_InstructorPrograms_programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterTemplates",
                columns: table => new
                {
                    SemesterTemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    TemplateID = table.Column<int>(type: "int", nullable: false),
                    CourseQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterTemplates", x => x.SemesterTemplateID);
                    table.ForeignKey(
                        name: "FK_SemesterTemplates_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterTemplates_Templates_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "Templates",
                        principalColumn: "TemplateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    ClassRoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingID = table.Column<int>(type: "int", nullable: false),
                    RoomNum = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.ClassRoomID);
                    table.ForeignKey(
                        name: "FK_Classroom_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorLoads",
                columns: table => new
                {
                    InstructorLoadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false),
                    Load = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorLoads", x => x.InstructorLoadID);
                    table.ForeignKey(
                        name: "FK_InstructorLoads_AspNetUsers_WNumber",
                        column: x => x.WNumber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorLoads_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceDetails",
                columns: table => new
                {
                    PreferenceDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModalityID = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekID = table.Column<int>(type: "int", nullable: true),
                    TimeOfDayID = table.Column<int>(type: "int", nullable: true),
                    CampusID = table.Column<int>(type: "int", nullable: true),
                    WNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferencesRank = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceDetails", x => x.PreferenceDetailsID);
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_AspNetUsers_WNumber",
                        column: x => x.WNumber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_Campus_CampusID",
                        column: x => x.CampusID,
                        principalTable: "Campus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_DayOfTheWeek_DayOfWeekID",
                        column: x => x.DayOfWeekID,
                        principalTable: "DayOfTheWeek",
                        principalColumn: "DayOfTheWeekID");
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_Modality_ModalityID",
                        column: x => x.ModalityID,
                        principalTable: "Modality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceDetails_TimeOfDays_TimeOfDayID",
                        column: x => x.TimeOfDayID,
                        principalTable: "TimeOfDays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReleaseTimes",
                columns: table => new
                {
                    ReleaseTimeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false),
                    ReleaseTimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseTimeNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseTimeAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseTimes", x => x.ReleaseTimeID);
                    table.ForeignKey(
                        name: "FK_ReleaseTimes_AspNetUsers_WNumber",
                        column: x => x.WNumber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleaseTimes_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_WNumber",
                        column: x => x.WNumber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterSchedules",
                columns: table => new
                {
                    SemesterScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterTemplateID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SemesterID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSchedules", x => x.SemesterScheduleID);
                    table.ForeignKey(
                        name: "FK_SemesterSchedules_SemesterTemplates_SemesterTemplateID",
                        column: x => x.SemesterTemplateID,
                        principalTable: "SemesterTemplates",
                        principalColumn: "SemesterTemplateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSchedules_Semester_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSchedules_statuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseSection",
                columns: table => new
                {
                    CourseSectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemesterScheduleID = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    WNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModalityID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    ClassRoomID = table.Column<int>(type: "int", nullable: false),
                    TimeSlotID = table.Column<int>(type: "int", nullable: false),
                    DayOfWeekID = table.Column<int>(type: "int", nullable: false),
                    PartOfTerm = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FirstDayEnrollment = table.Column<int>(type: "int", nullable: true),
                    ThirdWeekEnrollment = table.Column<int>(type: "int", nullable: true),
                    CRN = table.Column<int>(type: "int", nullable: true),
                    MaxEnrollment = table.Column<int>(type: "int", nullable: true),
                    SectionNotes = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    PayModelID = table.Column<int>(type: "int", nullable: false),
                    WhoPaysID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSection", x => x.CourseSectionID);
                    table.ForeignKey(
                        name: "FK_CourseSection_AspNetUsers_WNumber",
                        column: x => x.WNumber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseSection_Classroom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "Classroom",
                        principalColumn: "ClassRoomID");
                    table.ForeignKey(
                        name: "FK_CourseSection_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID");
                    table.ForeignKey(
                        name: "FK_CourseSection_DayOfTheWeek_DayOfWeekID",
                        column: x => x.DayOfWeekID,
                        principalTable: "DayOfTheWeek",
                        principalColumn: "DayOfTheWeekID");
                    table.ForeignKey(
                        name: "FK_CourseSection_Modality_ModalityID",
                        column: x => x.ModalityID,
                        principalTable: "Modality",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseSection_SemesterSchedules_SemesterScheduleID",
                        column: x => x.SemesterScheduleID,
                        principalTable: "SemesterSchedules",
                        principalColumn: "SemesterScheduleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSection_TimeSlots_TimeSlotID",
                        column: x => x.TimeSlotID,
                        principalTable: "TimeSlots",
                        principalColumn: "TimeSlotID");
                    table.ForeignKey(
                        name: "FK_CourseSection_WhoPays_WhoPaysID",
                        column: x => x.WhoPaysID,
                        principalTable: "WhoPays",
                        principalColumn: "WhoPaysID");
                    table.ForeignKey(
                        name: "FK_CourseSection_payModels_PayModelID",
                        column: x => x.PayModelID,
                        principalTable: "payModels",
                        principalColumn: "PayModelID");
                    table.ForeignKey(
                        name: "FK_CourseSection_statuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "statuses",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MajorId",
                table: "AspNetUsers",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WNumber",
                table: "AspNetUsers",
                column: "WNumber",
                unique: true,
                filter: "[WNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Building_CampusID",
                table: "Building",
                column: "CampusID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_BuildingID",
                table: "Classroom",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_ClassRoomID",
                table: "CourseSection",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_CourseID",
                table: "CourseSection",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_DayOfWeekID",
                table: "CourseSection",
                column: "DayOfWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_ModalityID",
                table: "CourseSection",
                column: "ModalityID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_PayModelID",
                table: "CourseSection",
                column: "PayModelID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_SemesterScheduleID",
                table: "CourseSection",
                column: "SemesterScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_StatusID",
                table: "CourseSection",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_TimeSlotID",
                table: "CourseSection",
                column: "TimeSlotID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_WhoPaysID",
                table: "CourseSection",
                column: "WhoPaysID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSection_WNumber",
                table: "CourseSection",
                column: "WNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorLoads_SemesterID",
                table: "InstructorLoads",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorLoads_WNumber",
                table: "InstructorLoads",
                column: "WNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPrograms_ProgramID",
                table: "InstructorPrograms",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_CampusID",
                table: "PreferenceDetails",
                column: "CampusID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_CourseID",
                table: "PreferenceDetails",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_DayOfWeekID",
                table: "PreferenceDetails",
                column: "DayOfWeekID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_ModalityID",
                table: "PreferenceDetails",
                column: "ModalityID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_SemesterID",
                table: "PreferenceDetails",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_TimeOfDayID",
                table: "PreferenceDetails",
                column: "TimeOfDayID");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceDetails_WNumber",
                table: "PreferenceDetails",
                column: "WNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseTimes_SemesterID",
                table: "ReleaseTimes",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseTimes_WNumber",
                table: "ReleaseTimes",
                column: "WNumber");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedules_SemesterID",
                table: "SemesterSchedules",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedules_SemesterTemplateID",
                table: "SemesterSchedules",
                column: "SemesterTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSchedules_StatusID",
                table: "SemesterSchedules",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTemplates_CourseID",
                table: "SemesterTemplates",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterTemplates_TemplateID",
                table: "SemesterTemplates",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_WNumber",
                table: "Tickets",
                column: "WNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassroomConfiguration");

            migrationBuilder.DropTable(
                name: "ClassroomDetail");

            migrationBuilder.DropTable(
                name: "CoursePrereqs");

            migrationBuilder.DropTable(
                name: "CourseSection");

            migrationBuilder.DropTable(
                name: "InstructorLoads");

            migrationBuilder.DropTable(
                name: "InstructorPrograms");

            migrationBuilder.DropTable(
                name: "PreferenceDetails");

            migrationBuilder.DropTable(
                name: "ReleaseTimes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropTable(
                name: "SemesterSchedules");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "WhoPays");

            migrationBuilder.DropTable(
                name: "payModels");

            migrationBuilder.DropTable(
                name: "programs");

            migrationBuilder.DropTable(
                name: "DayOfTheWeek");

            migrationBuilder.DropTable(
                name: "Modality");

            migrationBuilder.DropTable(
                name: "TimeOfDays");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "SemesterTemplates");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "Campus");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
