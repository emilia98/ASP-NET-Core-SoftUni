﻿using HomeForMe.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeForMe.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Property> Properties { get; set; }
    }
}
