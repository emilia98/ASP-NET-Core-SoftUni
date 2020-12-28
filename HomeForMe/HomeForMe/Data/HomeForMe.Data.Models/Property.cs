using HomeForMe.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeForMe.Data.Models
{
    public class Property : IDeletableEntity
    {
        public Property()
        {
            this.Wishlists = new List<Wishlist>();
        }

        public int Id { get; set; }

        public int? Bedrooms { get; set; }

        public int? Bathrooms { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool ForRent { get; set; }

        [Required]
        public DateTime AddedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual AppUser User { get; set; }

        [Required]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        [Required]
        public int PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
