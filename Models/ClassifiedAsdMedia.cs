using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class ClassifiedAsdMedia
    {
        [Key]
        public int MediaId { get; set; }
        public string MediaUrl { get; set; }
        public DateTime MediaDate { get; set; }
        [JsonIgnore]
        public virtual ClassifiedAds ClassifiedAds { get; set; }
        public int ClassifiedAdsID { get; set; }
    }
}
