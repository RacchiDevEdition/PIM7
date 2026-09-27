namespace PIM.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int UsersCount { get; set; }
        public int StudentsCount { get; set; }
        public int TeachersCount { get; set; }
        public int CoursesCount { get; set; }
        public int LessonsCount { get; set; }
        public string? CurrentUserName { get; set; }
    }
}
