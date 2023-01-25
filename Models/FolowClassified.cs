using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class FolowClassified
    {
        [Key]
        public int FolowClassifiedId { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public virtual ClassifiedAds ClassifiedAds { get; set; }
        public int ClassifiedAdsID { get; set; }
    }
}
