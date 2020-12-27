using System.ComponentModel.DataAnnotations;

namespace HomeForMe.InputModels.Auth
{
    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
