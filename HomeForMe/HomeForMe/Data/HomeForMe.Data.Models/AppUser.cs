﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HomeForMe.Data.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            this.Wishlists = new List<Wishlist>();
            this.UserRoles = new List<AppUserRole>();
        }

        public ICollection<Wishlist> Wishlists { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
