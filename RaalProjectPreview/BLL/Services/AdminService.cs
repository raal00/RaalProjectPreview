using RaalProjectPreview.BLL.Models.Admin;
using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.DAL.Enums;
using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.DAL.Repository;
using System;
using System.Collections.Generic;
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
        public AdminService()
        {
            _authUserDataReposirory = new AuthUserDataReposirory();
            _customerRepository = new CustomerRepository();
            _itemRepository = new ItemRepository();
            _orderItemRepository = new OrderItemRepository();
            _user_RoleRepository = new User_RoleRepository();
            _orderRepository = new OrderRepository();
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
                            AuthUserData = login,
                            Customer = customer,
                            UserRole = role
                        }
                        ).ToList();
            if (response == null) response = new List<AllUserData>();
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
        public ServiceResponseStatus AddNewUser(Customer customer, AuthUserData authUserData, UserRole role)
        {
            //добавить трай
            //проверка customer.Code
            customer = _customerRepository.Create(customer);
            authUserData.CustomerId = customer.Id;
            _authUserDataReposirory.Create(authUserData);
            role.CustomerId = customer.Id;
            //проверка role.ClientRole
            _user_RoleRepository.Create(role);
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Edit data of selected user
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="authUserData"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ServiceResponseStatus EditUser(Customer customer, AuthUserData authUserData, UserRole role) 
        {
            try
            {
                Customer _customer = _customerRepository.UpdateCustomer(customer);
                
                int authId = _authUserDataReposirory.GetByUserId(customer.Id).Id;
                authUserData.Id = authId;
                authUserData.CustomerId = customer.Id;
                AuthUserData _authUserData = _authUserDataReposirory.UpdateAuthUserData(authUserData);

                int userRoleId = _user_RoleRepository.GetByCustomerId(customer.Id).Id;
                role.CustomerId = customer.Id;
                role.Id = userRoleId;
                UserRole _userRole = _user_RoleRepository.UpdateUserRoleModel(role);
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
                if (customer == null) return ServiceResponseStatus.Completed;
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
            order.Status = OrderStatus.InProcessing.ToString();
            _orderRepository.UpdateOrder(order);
            return ServiceResponseStatus.Completed;
        }
    }
}