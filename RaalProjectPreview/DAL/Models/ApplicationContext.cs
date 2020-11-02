using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace RaalProjectPreview.DAL.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("BaseContext")
        {
            if (Customers.ToList().Count == 0)
            {
                Customer admin = new Customer();
                admin.Id = 1;
                admin.Code = "XXXX";
                admin.Address = "XXXX";
                admin.Discount = 0;
                admin.Name = "Admin";
                Customer customer = new Customer();
                customer.Id = 2;
                customer.Code = "XXXX";
                customer.Address = "XXXX";
                customer.Discount = 0;
                customer.Name = "Admin";

                admin = Customers.Add(admin);
                customer = Customers.Add(customer);

                UserRole userRoleAdmin = new UserRole();
                userRoleAdmin.ClientRole = Security.Roles.ClientRole.Manager;
                userRoleAdmin.CustomerId = admin.Id;
                UserRole userRoleCustomer = new UserRole();
                userRoleCustomer.ClientRole = Security.Roles.ClientRole.Customer;
                userRoleCustomer.CustomerId = customer.Id;

                UserRoles.Add(userRoleAdmin);
                UserRoles.Add(userRoleCustomer);

                AuthUserData authAdmin = new AuthUserData();
                authAdmin.CustomerId = admin.Id;
                authAdmin.Login = "Admin";
                authAdmin.PasswordHash = "Admin".GetHashCode().ToString();
                AuthUserData authCustomer = new AuthUserData();
                authCustomer.CustomerId = customer.Id;
                authCustomer.Login = "Customer";
                authCustomer.PasswordHash = "Customer".GetHashCode().ToString();

                AuthUserDatas.Add(authAdmin);
                AuthUserDatas.Add(authCustomer);
                SaveChanges();
            }
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
        public DbSet<CustomerCaseItem> CustomerCaseItems { get; set; }
    }
}