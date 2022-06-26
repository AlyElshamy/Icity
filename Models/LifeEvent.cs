using Icity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Icity.Models
{
    public class LifeEvent
    {
        [Key]
        public int LifeEventID { get; set; }
        //[Required(ErrorMessage = "Is Required")]
        public string EventType { get; set; }
        //[Required(ErrorMessage = "Is Required")] 
        public string Caption { get; set; }
        //[Required(ErrorMessage = "Is Required")] 
        public string Details { get; set; }
        //[Required(ErrorMessage = "Is Required")]
        public string Media { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
    }
}
