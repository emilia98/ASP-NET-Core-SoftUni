using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HomeForMe.Data.Models
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole()
        {
            this.UserRoles = new List<AppUserRole>();
        }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
