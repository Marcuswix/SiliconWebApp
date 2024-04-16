using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class UserCourseItemEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int CourseId { get; set; }
    }
}
