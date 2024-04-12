using Infrastructure.Entities;

namespace Infrastructure.ViewModels
{
    public class CourseDetailsViewModel
    {
        public TeacherEntity Teacher { get; set; } = new TeacherEntity();

        public CourseEntity Course { get; set; } = new CourseEntity();

    }
}
