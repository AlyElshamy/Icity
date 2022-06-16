using Icity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class Education
    {
        [Key]
        public int EducationID { get; set; }
        public int Year { get; set; }
        public string Provider { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
    }
}
