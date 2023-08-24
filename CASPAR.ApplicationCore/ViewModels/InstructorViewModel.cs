namespace CASPAR.ApplicationCore.Models.ViewModels
{
    public class InstructorViewModel
    {
        public string WNumber { get; set; }
        //public int ProgramPreference { get; set; }
        public string ProgramPreference { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? SpringLoad { get; set; }
        public int? SummerLoad { get; set; }
        public int? FallLoad { get; set; }
        public string Roles { get; set; }
        public string Details { get; set; }

        public string Url { get; set; }
    }
}