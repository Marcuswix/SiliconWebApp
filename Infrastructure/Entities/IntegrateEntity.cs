using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class IntegrateEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(400)]
        public string Ingress { get; set; } = null!;

        public ICollection<IntegrateItemEntity> IntegrateItems { get; set;  } = new List<IntegrateItemEntity>();
    }
}
