﻿using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Razor;
using RaalProjectPreview.DAL.Enums;
using System.Security.Cryptography;

namespace RaalProjectPreview.BLL.Services
{
    public class CustomerService
    {
        private readonly AuthUserDataReposirory _authUserDataReposirory;
        private readonly CustomerRepository _customerRepository;
        private readonly ItemRepository _itemRepository;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly User_RoleRepository _user_RoleRepository;
        private readonly OrderRepository _orderRepository;
        private readonly CustomerCaseItemRepository _customerCaseItemRepository;
        public CustomerService()
        {
            _authUserDataReposirory = new AuthUserDataReposirory();
            _customerRepository = new CustomerRepository();
            _itemRepository = new ItemRepository();
            _orderItemRepository = new OrderItemRepository();
            _user_RoleRepository = new User_RoleRepository();
            _orderRepository = new OrderRepository();
            _customerCaseItemRepository = new CustomerCaseItemRepository();
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            List<Order> response = _orderRepository.GetList(x => x.CustomerId == customerId);
            return response;
        }
        public List<Item> GetMyCase(int userId)
        {
            List<Item> caseItems = (from _case in _customerCaseItemRepository.GetList(x => x.CustomerId == userId)
                                    join _item in _itemRepository.GetAll() on _case.ItemId equals _item.Id into j
                                    from subcase in j.DefaultIfEmpty()
                                    select subcase).ToList();
            return caseItems;
        }
        public ResponseStatus AddItemToCase(int itemId, int userId)
        {
            Item item = _itemRepository.GetItemById(itemId);
            if (item == null) return ResponseStatus.Failed;
            CustomerCaseItem caseItem = new CustomerCaseItem();
            caseItem.CustomerId = itemId;
            caseItem.ItemId = itemId;
            _customerCaseItemRepository.Create(caseItem);
            return ResponseStatus.Completed;
        }
        public Order CreateOrder(int userId)
        {
            List<CustomerCaseItem> itemsPerCase = _customerCaseItemRepository.GetList(x => x.CustomerId == userId);
            if (itemsPerCase == null || itemsPerCase.Count == 0)
            {
                return null;
            }
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var itemPerCase in itemsPerCase)
            {

            }

            Order order = new Order();
            order.CustomerId = userId;
            order.OrderDate = DateTime.Now;
            order.ShipmentDate = DateTime.Now.AddDays(7);
            order.Status = OrderStatus.New.ToString();
            order = _orderRepository.Create(order);
            
            return order;
        }
        public List<Item> GetItemList()
        {
            List<Item> items = _itemRepository.GetAll();
            return items;
        }
        public ResponseStatus RemoveOrder(int orderId, int userId)
        {
            Order order = _orderRepository.GetOrderById(orderId);
            if (order == null || 
                order.CustomerId != userId ||
                order.Status != OrderStatus.New.ToString()) 
                return ResponseStatus.Failed;
            _orderRepository.Delete(order);
            return ResponseStatus.Completed;
        }
    }
}