using Icity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class FavouriteProfile
    {
        [Key]
        public int FavouriteProfileId { get; set; }
        public string UserId { get; set; }
       
        public string Id { get; set; }
    }
}
