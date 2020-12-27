using System.ComponentModel.DataAnnotations;

namespace HomeForMe.InputModels.Auth
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
