using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Icity.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        [Required(ErrorMessage = "Reequired")]
        public string Title { get; set; }
        [JsonIgnore]
        public virtual AddListing AddListing { get; set; }
        public int AddListingId { get; set; }

    }
}
