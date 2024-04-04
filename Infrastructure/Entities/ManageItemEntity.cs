using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ManageItemEntity
    {
        public int Id { get; set; }

        public int ManagerId { get; set; }

        public ManageEntity Manager { get; set; } = null!;

        [StringLength(50)]
        public string Facts { get; set; } = null!;

    }
}
