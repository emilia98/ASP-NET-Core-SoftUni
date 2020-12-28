using HomeForMe.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class Location : IDeletableEntity
    {
        public Location()
        {
            this.Properties = new List<Property>();
        }

        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        public ICollection<Property> Properties { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
