using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesRecordManagementSystem.Models;
using System.Reflection;

namespace SalesRecordManagementSystem.Context
{
    public class SRSContext : IdentityDbContext<User>
    {
        public SRSContext(DbContextOptions<SRSContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        new public DbSet<User> Users { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

