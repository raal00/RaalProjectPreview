using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public Customer GetCustomerById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public Customer UpdateCustomer(Customer customer)
        {
            Customer _updatedCustomer = GetCustomerById(customer.Id);
            _updatedCustomer.Name = customer.Name;
            _updatedCustomer.Code = customer.Code;
            _updatedCustomer.Discount = customer.Discount;
            _updatedCustomer.Address = customer.Address;
            _APPContext.SaveChanges();
            return _updatedCustomer;
        }
    }
}