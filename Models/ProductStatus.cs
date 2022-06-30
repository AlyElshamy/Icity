using System.ComponentModel.DataAnnotations;

namespace Icity.Models
{
    public class ProductStatus
    {
        [Key]
        public int ProductStatusID { get; set; }
        public string StatusTitle { get; set; }

    }
    //Seeding
}
