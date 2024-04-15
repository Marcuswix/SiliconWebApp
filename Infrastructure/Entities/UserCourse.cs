namespace Infrastructure.Entities
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
    }
}
