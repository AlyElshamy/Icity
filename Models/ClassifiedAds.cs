using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class ClassifiedAds
    {
        [Key]
        public int ClassifiedAdsID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public string AddedBy { get; set; }
        public bool Status { get; set; }
        public string MainPhoto { get; set; }
        /// <StatusProperity>
        /// It has been purchased or not ... so that if it is purchased,
        /// it will be deleted from the page that shows the user who is buying..
        /// this Faild Will Be Have avalue 1 if User Purchase This Product
        /// </StatusProperity>
        public DateTime AddedDate { get; set; }
        public DateTime? PayedDate { get; set; }//will be Filled After Is Payed
        public string  ClassifiedAdsLocation { get; set; }
        [JsonIgnore]
        public virtual ClassifiedAdsType ClassifiedAdsType { get; set; }
        public int ClassifiedAdsTypeID { get; set; }
        [JsonIgnore]
        public virtual ProductStatus ProductStatus { get; set; }
        public int ProductStatusID { get; set; }
        public virtual ICollection<ClassifiedAsdMedia> ClassifiedAsdMedias { get; set; }

    }
}
