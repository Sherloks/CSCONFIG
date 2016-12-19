﻿
using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ConfigurationAttribute> ConfigurationAttributes { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        public DbSet<AttributeCategory> AttributeCategories { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Setting>().ToTable("Settings");
            modelBuilder.Entity<ConfigurationAttribute>().ToTable("ConfigurationAttributes");
            modelBuilder.Entity<Attribute>().ToTable("Attributes");
            modelBuilder.Entity<AttributeType>().ToTable("AttributeTypes");
            modelBuilder.Entity<AttributeCategory>().ToTable("AttributeCategories");
            modelBuilder.Entity<Configuration>().ToTable("Configurations");
        }
    }
}
