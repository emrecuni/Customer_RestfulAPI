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
            // Many-to-many: Policy ↔ Customer (insured)
            modelBuilder.Entity<Policy>()
                .HasMany(p => p.InsuredList)
                .WithMany(c => c.PoliciesAsInsured)
                .UsingEntity(j => j.ToTable("PolicyInsuredCustomers"));

            modelBuilder.Entity<Policy>()
                .HasOne(p => p.Insurer)
                .WithMany() // Customer → Policy için navigasyon tanımlamadık
                .HasForeignKey(p => p.InsurerId)
                .OnDelete(DeleteBehavior.Restrict); // circular dependency riskine karşı
        }
    }
}
