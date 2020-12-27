using HomeForMe.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeForMe.Data.Seeding
{
    public class PropertyTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedPropertyTypesAsync(dbContext);
        }

        public static async Task SeedPropertyTypesAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.PropertyTypes.AnyAsync())
            {
                return;
            }

            var propertyTypes = new List<PropertyType>
            {
                new PropertyType { Name = "One room" },
                new PropertyType { Name = "Two rooms" },
                new PropertyType { Name = "Three rooms" },
                new PropertyType { Name = "Office" }
            };

            await dbContext.PropertyTypes.AddRangeAsync(propertyTypes);
        }
    }
}
