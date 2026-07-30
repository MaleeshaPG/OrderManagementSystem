using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;
using System;

namespace OrderManagementSystem.Data
{
    public class OMSDbContext: IdentityDbContext<ApplicationUser>
    {
        public OMSDbContext(DbContextOptions<OMSDbContext> options)
       : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SubDepartment> SubDepartments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<OrderGroup> OrderGroups { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<StoreOrderGroup> StoreOrderGroups { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }
        public DbSet<StoreItemSupplier> StoreItemSuppliers { get; set; }
        public DbSet<ForecastData> ForecastData { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(OMSDbContext).Assembly
            );
        }

    }
}
