using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class AppUser
    {
        public AppUser()
        {
            this.Wishlists = new List<Wishlist>();
        }

        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; }
    }
}
