using System.ComponentModel.DataAnnotations;

namespace Icity.Models
{
    public class EntityType
    {
        [Key]
        public int EntityTypeId { get; set; }
        public string EntityTitle { get; set; }
    }
}
