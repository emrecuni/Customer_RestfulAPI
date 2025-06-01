using Microsoft.EntityFrameworkCore;
using System;
using Customer_RestfulAPI.Models;

namespace Customer_RestfulAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Policy> Policies => Set<Policy>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many ilişki
            modelBuilder.Entity<Policy>()
                .HasMany(p => p.InsuredList)
                .WithMany(c => c.PoliciesAsInsured)
                .UsingEntity(j => j.ToTable("PolicyInsured"));

            // One-to-many ilişki
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.PoliciesAsInsurer)
                .WithOne(p => p.Insurer)
                .HasForeignKey(p => p.InsurerId);
        }
    }
}
