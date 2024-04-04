using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ManageEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string AltText { get; set; } = null!;
        public ICollection<ManageItemEntity> Tags { get; set; } = new HashSet<ManageItemEntity>();

    }
}
