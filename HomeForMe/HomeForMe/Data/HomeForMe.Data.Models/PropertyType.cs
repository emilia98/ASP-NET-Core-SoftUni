using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class PropertyType
    {
        public PropertyType()
        {
            this.Properties = new List<Property>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
