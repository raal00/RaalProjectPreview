using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using Effort;
using RaalProjectPreview.DAL.Models.DBModels;

namespace RaalProjectPreview.Tests
{
    public class DatabaseTestContext : DbContext
    {
        public DatabaseTestContext(DbConnection connection) : base(connection, false)
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AuthUserData> AuthUserDatas { get; set; }
        public DbSet<CustomerCaseItem> CustomerCaseItems { get; set; }
    }
}
