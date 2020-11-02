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
using System.Data.Entity;

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
        public CustomerService(DbContext context)
        {
            _authUserDataReposirory = new AuthUserDataReposirory(context);
            _customerRepository = new CustomerRepository(context);
            _itemRepository = new ItemRepository(context);
            _orderItemRepository = new OrderItemRepository(context);
            _user_RoleRepository = new User_RoleRepository(context);
            _orderRepository = new OrderRepository(context);
            _customerCaseItemRepository = new CustomerCaseItemRepository(context);
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            List<Order> response = _orderRepository.GetList(x => x.CustomerId == customerId);
            return response;
        }
        public List<Item> GetMyCase(int userId)
        {
            List<Item> caseItems = (from _case in _customerCaseItemRepository.GetList(x => x.CustomerId == userId)
                                    join _item in _itemRepository.GetAll()  on _case.ItemId equals _item.Id
                                    select _item).ToList();
            return caseItems;
        }
        public ResponseStatus AddItemToCase(int itemId, int userId)
        {
            Item item = _itemRepository.GetItemById(itemId);
            if (item == null) return ResponseStatus.Failed;
            CustomerCaseItem caseItem = new CustomerCaseItem();
            caseItem.CustomerId = userId;
            caseItem.ItemId = itemId;
            try
            {
                _customerCaseItemRepository.Create(caseItem);
            }
            catch (Exception er)
            {
                return ResponseStatus.Failed;
            }
            return ResponseStatus.Completed;
        }
        public ResponseStatus CreateOrder(int userId)
        {
            List<CustomerCaseItem> itemsPerCase = _customerCaseItemRepository.GetList(x => x.CustomerId == userId);
            if (itemsPerCase == null || itemsPerCase.Count == 0)
            {
                return ResponseStatus.Failed;
            }
            Order order = new Order();
            order.CustomerId = userId;
            order.OrderDate = DateTime.Now;
            order.ShipmentDate = DateTime.Now.AddDays(7);
            order.Status = OrderStatus.New.ToString();
            try
            {
                order = _orderRepository.Create(order);
            }
            catch(Exception er)
            {
                return ResponseStatus.Failed;
            }
            var orderItems = (from itemPerCase in itemsPerCase
                              group itemPerCase by itemPerCase.ItemId).ToList();
            foreach (IGrouping<int, CustomerCaseItem> oitem in orderItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ItemsCount = oitem.Count();
                orderItem.ItemPrice = _itemRepository.GetItemById(oitem.Key).Price;
                orderItem.ItemId = oitem.Key;
                orderItem.OrderId = order.Id;
                try
                {
                    _orderItemRepository.Create(orderItem);
                }
                catch (Exception er)
                {
                    return ResponseStatus.Warning;
                }
            }
            _customerCaseItemRepository.RemoveCaseByUserId(userId);
            return ResponseStatus.Completed;
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
            try
            {
                _orderItemRepository.RemoveOrderItemsByOrderId(orderId);
            }
            catch(Exception er)
            {
                return ResponseStatus.Failed;
            }
            return ResponseStatus.Completed;
        }
    }
}