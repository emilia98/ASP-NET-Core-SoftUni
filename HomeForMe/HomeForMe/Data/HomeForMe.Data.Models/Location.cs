using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class Location
    {
        public Location()
        {
            this.Properties = new List<Property>();
        }

        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
