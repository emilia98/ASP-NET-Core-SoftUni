using HomeForMe.Data.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HomeForMe.Data.Models
{
    public class AppRole : IdentityRole<int>, IDeletableEntity
    {
        public AppRole()
        {
            this.UserRoles = new List<AppUserRole>();
        }

        public ICollection<AppUserRole> UserRoles { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
