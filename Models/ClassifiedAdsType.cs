using System.ComponentModel.DataAnnotations;

namespace Icity.Models
{
    public class ClassifiedAdsType
    {
        [Key]
        public int ClassifiedAdsTypeID { get; set; }
        public string TypeTitleAr { get; set; }
        public string TypeTitleEn { get; set; }
        public string TypePic { get; set; }
        public int? SortOrder { get; set; }
    }
    //Seeding
}
