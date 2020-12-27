using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }
    }
}
