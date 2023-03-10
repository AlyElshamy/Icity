using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Icity.Models
{
    public class ListingPhotos
    {
        [Key]
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string Caption { get; set; }
        public DateTime PublishDate { get; set; }
        [JsonIgnore]
        public virtual AddListing AddListing { get; set; }
        public int AddListingId { get; set; }
    }
}
