﻿using Microsoft.AspNetCore.Identity;

namespace HomeForMe.Data.Models
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; }

        public AppRole Role { get; set; }
    }
}
