using System.ComponentModel.DataAnnotations;

namespace HomeForMe.InputModels.Admin
{
    public class LocationInputModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "City should be at most 30 characters long!")]
        public string City { get; set; }
    }
}
