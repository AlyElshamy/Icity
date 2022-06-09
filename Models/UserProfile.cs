using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Icity.Models
{
    public class UserProfile
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Qualification { get; set; }
        public string Job { get; set; }
        public string Gender { get; set; }
        public string ProfilePicture { get; set; }
        public string Profilebanner { get; set; }
    }
}
