using RaalProjectPreview.BLL.Models.Admin;
using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.DAL.Enums;
using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RaalProjectPreview.BLL.Services
{
    public class AdminService
    {
        private readonly AuthUserDataReposirory _authUserDataReposirory;
        private readonly CustomerRepository _customerRepository;
        private readonly ItemRepository _itemRepository;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly User_RoleRepository _user_RoleRepository;
        private readonly OrderRepository _orderRepository;
        public AdminService(DbContext context)
        {
            _authUserDataReposirory = new AuthUserDataReposirory(context);
            _customerRepository = new CustomerRepository(context);
            _itemRepository = new ItemRepository(context);
            _orderItemRepository = new OrderItemRepository(context);
            _user_RoleRepository = new User_RoleRepository(context);
            _orderRepository = new OrderRepository(context);
        }
        public bool IsAdmin(int userId)
        {
            return _user_RoleRepository.GetRoleByCustomerId(userId) == Security.Roles.ClientRole.Manager;
        }
        public List<Item> ShowItems()
        {
            List<Item> response;
            response = (from item in _itemRepository.GetAll()
                        select item).ToList();
            if (response == null) response = new List<Item>();
            return response;
        }
        public List<Order> ShowOrders()
        {
            List<Order> response;
            response = (from order in _orderRepository.GetAll()
                        select order).ToList();
            if (response == null) response = new List<Order>();
            return response;
        }
        public List<AllUserData> ShowUsers()
        {
            List<AllUserData> response;
            response = (from customer in _customerRepository.GetAll()
                        join role in _user_RoleRepository.GetAll() on customer.Id equals role.CustomerId
                        join login in _authUserDataReposirory.GetAll() on customer.Id equals login.CustomerId
                        select new AllUserData
                        {
                            CustomerId = customer.Id,
                            ClientRole = role.ClientRole, 
                            Address = customer.Address, 
                            Code = customer.Code, 
                            Discount = customer.Discount, 
                            Login = login.Login, 
                            Name = customer.Name, 
                            PasswordHash = login.PasswordHash
                        }
                        ).ToList();
            return response;
        }

        /// <summary>
        /// Add new shop item to database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ServiceResponseStatus AddNewItem(Item item)
        {
            try
            {
                Random random = new Random();
                string date = DateTime.Now.Year.ToString();
                string code1 = random.Next(10, 99).ToString();
                string code2 = _itemRepository.GetLastItemId().ToString();
                item.Code = $"{code1}-{date}-AA{code2}";
                _itemRepository.Create(item);
            }
            catch (Exception er)
            {
                return ServiceResponseStatus.Failure;
            }
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Delete selected item from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponseStatus DeleteItemFromShop(int id)
        {
            try
            {
                Item item = _itemRepository.GetItemById(id);
                _itemRepository.Delete(item);
            }
            catch(Exception er)
            {
                return ServiceResponseStatus.Failure;
            }
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Edit selected item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ServiceResponseStatus EditItemInShop(Item item)
        {
            try
            {
                _itemRepository.UpdateItem(item);
            }
            catch(Exception er)
            {
                return ServiceResponseStatus.Failure;
            }
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Add data of new user
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="authUserData"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ServiceResponseStatus AddNewUser(AllUserData customer)
        {
            
            string date = DateTime.Now.Year.ToString();
            string lastCode = _customerRepository.GetIdOfLastUser().ToString();
            while(lastCode.Length < 4) lastCode += '0';
            customer.Code = date + "-" + lastCode;
            Customer newCustomer = new Customer();
            newCustomer.Code = customer.Code;
            newCustomer.Address = customer.Address;
            newCustomer.Discount = customer.Discount;
            newCustomer.Name = customer.Name;
            try
            {
                newCustomer = _customerRepository.Create(newCustomer);
            }
            catch(Exception er)
            {
                return ServiceResponseStatus.Failure;
            }
            AuthUserData newAuth = new AuthUserData();
            newAuth.CustomerId = newCustomer.Id;
            newAuth.Login = customer.Login;
            newAuth.PasswordHash = customer.PasswordHash;
            if (newAuth.Login == null || newAuth.PasswordHash == null) 
            {
                _customerRepository.Delete(newCustomer);
                return ServiceResponseStatus.Failure; 
            }
            _authUserDataReposirory.Create(newAuth);
            UserRole newRole = new UserRole();
            newRole.CustomerId = newCustomer.Id;
            newRole.ClientRole = customer.ClientRole;
            if ((int)newRole.ClientRole < 0 || (int)newRole.ClientRole > 1) newRole.ClientRole = Security.Roles.ClientRole.Customer;
            _user_RoleRepository.Create(newRole);
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Edit data of selected user
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="authUserData"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ServiceResponseStatus EditUser(AllUserData customer) 
        {
            try
            {
                Customer editCustomer = _customerRepository.GetById(customer.CustomerId);
                editCustomer.Name = customer.Name;
                editCustomer.Discount = customer.Discount;
                editCustomer.Code = customer.Code;
                editCustomer.Address = customer.Address;
                editCustomer = _customerRepository.UpdateCustomer(editCustomer);

                AuthUserData editAuth = _authUserDataReposirory.GetByUserId(editCustomer.Id);
                editAuth.Login = customer.Login;
                editAuth.PasswordHash = customer.PasswordHash;
                editAuth = _authUserDataReposirory.UpdateAuthUserData(editAuth);

                UserRole editRole = _user_RoleRepository.GetByCustomerId(editCustomer.Id);
                editRole.ClientRole = customer.ClientRole;
                if ((int)editRole.ClientRole < 0 || (int)editRole.ClientRole > 1) editRole.ClientRole = Security.Roles.ClientRole.Customer;
                UserRole _userRole = _user_RoleRepository.UpdateUserRoleModel(editRole);
            }
            catch(Exception er)
            {
                return ServiceResponseStatus.Failure;
            }
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Delete selected user
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ServiceResponseStatus DeleteUser(int UserId) 
        {
            try
            {
                Customer customer = _customerRepository.GetById(UserId);
                if (customer == null) return ServiceResponseStatus.Failure;
                _customerRepository.Delete(customer);
                UserRole userRole = _user_RoleRepository.GetByCustomerId(UserId);
                if (userRole != null) _user_RoleRepository.Delete(userRole);
                AuthUserData authUserData = _authUserDataReposirory.GetByUserId(UserId);
                if (authUserData != null) _authUserDataReposirory.Delete(authUserData);
            }
            catch(Exception er)
            {
                return ServiceResponseStatus.Failure;
            }
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Set order complete status by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ServiceResponseStatus SetCompletedOrderStatus(int orderId)
        {
            Order order = _orderRepository.GetOrderById(orderId);
            if (order == null) return ServiceResponseStatus.Failure;
            order.Status = OrderStatus.Completed.ToString();
            _orderRepository.UpdateOrder(order);
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Set order InProcessing status by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ServiceResponseStatus SetInProcessingOrderStatus(int orderId) 
        {
            Order order = _orderRepository.GetOrderById(orderId);
            if (order == null) return ServiceResponseStatus.Failure;
            if (order.Status == OrderStatus.Completed.ToString()) return ServiceResponseStatus.Failure;
            order.Status = OrderStatus.InProcessing.ToString();
            _orderRepository.UpdateOrder(order);
            return ServiceResponseStatus.Completed;
        }
    }
}