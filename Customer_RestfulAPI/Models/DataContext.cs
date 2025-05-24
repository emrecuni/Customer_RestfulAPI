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
    }
}
