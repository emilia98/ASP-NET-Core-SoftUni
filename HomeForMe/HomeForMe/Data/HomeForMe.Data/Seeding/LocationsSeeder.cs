using HomeForMe.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeForMe.Data.Seeding
{
    internal class LocationsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedLocationsAsync(dbContext);
        }

        private static async Task SeedLocationsAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.Locations.AnyAsync())
            {
                return;
            }

            var locations = new List<Location>
            {
                new Location { City = "Sofia" },
                new Location { City = "Plovdiv" },
                new Location { City = "Varna" },
                new Location { City = "Gabrovo" }
            };

            await dbContext.Locations.AddRangeAsync(locations);
        }
    }
}
