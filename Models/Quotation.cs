using Icity.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class Quotation
    {
        public int QuotationId { get; set; }
        public DateTime QuotationDate { get; set; }
        public string Description { get; set; }
       
        public string UserId { get; set; }
        public int ClassifiedAdsID { get; set; }
        [JsonIgnore]
        public virtual ClassifiedAds ClassifiedAds { get; set; }


    }
}
