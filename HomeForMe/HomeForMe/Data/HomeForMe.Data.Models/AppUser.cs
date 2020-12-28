using HomeForMe.Data.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HomeForMe.Data.Models
{
    public class AppUser : IdentityUser<int>, IDeletableEntity
    {
        public AppUser()
        {
            this.Wishlists = new List<Wishlist>();
            this.UserRoles = new List<AppUserRole>();
        }

        public ICollection<Wishlist> Wishlists { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
