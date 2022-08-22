using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Icity.ViewModel
{
    public class ReviewVM
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
        public int AddListingId { get; set; }
    }
}
