using DemoTask.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.DAL
{
    public class DataContext : IdentityDbContext<User>
    { 
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<LoginToken> LoginToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            //modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }
}
