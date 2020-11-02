using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace RaalProjectPreview.DAL.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(DbContext context) : base(context)
        {

        }
        public Customer GetCustomerById(int id)
        {
            return _DbSet.Where(x => x.Id == id).FirstOrDefault();
        }
        public Customer UpdateCustomer(Customer customer)
        {
            Customer _updatedCustomer = GetCustomerById(customer.Id);
            if (_updatedCustomer == null) throw new NullReferenceException($"customer with id={customer.Id} not found");
            _updatedCustomer.Name = customer.Name;
            _updatedCustomer.Code = customer.Code;
            _updatedCustomer.Discount = customer.Discount;
            _updatedCustomer.Address = customer.Address;
            _APPContext.SaveChanges();
            return _updatedCustomer;
        }

        public string GetCustomerNameById(int id)
        {
            return (from user in _DbSet
                    where user.Id == id
                    select user.Name).FirstOrDefault();
        }
        public Customer GetById(int id)
        {
            return (from user in _DbSet 
                    where user.Id == id 
                    select user).FirstOrDefault();
        }
        public int GetIdOfLastUser()
        {
            Customer customer = _DbSet.ToList().LastOrDefault();
            return customer == null ? 0 : customer.Id;
        }
    }
}