using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class PropertyType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
