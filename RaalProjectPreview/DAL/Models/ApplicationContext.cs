using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace RaalProjectPreview.DAL.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("BaseContext")
        {
            
        }

        private static ApplicationContext _instance;
        public static ApplicationContext GetInstance()
        {
            if (_instance == null)
            _instance = new ApplicationContext();
            return _instance;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<ApplicationContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AuthUserData> AuthUserDatas { get; set; }
    }
}