using System;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        [Required]
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual AppUser User { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
