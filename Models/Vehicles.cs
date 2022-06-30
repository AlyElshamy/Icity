using Icity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Icity.Models
{
    public class Vehicles:ClassifiedAds
    {
        [Key]
        public int VehiclesID { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Speed { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public ICollection<ListingPhotos> ListingPhotos { get; set; }
        public ICollection<ListingVideos> ListingVideos { get; set; }
    }
}
