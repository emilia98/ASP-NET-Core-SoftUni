using System;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.InputModels.Property
{
    public class NewPropertyInputModel
    {
        [Required]
        public int? Location { get; set; }

        [Required]
        public int? Type { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Price should be a value between 0 and 100000!")]
        public decimal? Price { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Bedrooms should be a value between 0 and 20!")]
        public int? Bedrooms { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Bathrooms should be a value between 0 and 20!")]
        public int? Bathrooms { get; set; }

        public string Description { get; set; }
    }
}
