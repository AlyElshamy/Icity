using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Icity.Models
{
    public class AddListing
    {
        public int AddListingId { get; set; }
        public string CreatedByUser { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string MainLocataion { get; set; }
        public string Address { get; set; }
        public string ContactPeroson { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Discription { get; set; }
        public string Tags { get; set; }
        public double Rating { get; set; }
        public string ListingBanner { get; set; }
        public string ListingLogo { get; set; }
        public string PromoVideo { get; set; }
        public ICollection<Branch> Branches { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ListingPhotos> ListingPhotos { get; set; }
        public ICollection<ListingVideos> ListingVideos { get; set; }
    }
}
