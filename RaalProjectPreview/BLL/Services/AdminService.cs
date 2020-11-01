using RaalProjectPreview.BLL.Models.Admin;
using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;

namespace RaalProjectPreview.BLL.Services
{
    public class AdminService
    {
        public AdminService()
        {

        }

        public List<Item> ShowItems()
        {
            List<Item> response = new List<Item>();
            return response;
        }
        public List<Order> ShowOrders()
        {
            List<Order> response = new List<Order>();
            return response;
        }
        public List<AllUserData> ShowUsers()
        {
            List<AllUserData> response = new List<AllUserData>();
            return response;
        }

        /// <summary>
        /// Add new shop item to database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ServiceResponseStatus AddNewItem(Item item)
        {
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Delete selected item from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponseStatus DeleteItemFromShop(int id)
        {
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Edit selected item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ServiceResponseStatus EditItemInShop(Item item)
        {
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
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Delete selected user
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ServiceResponseStatus DeleteUser(int UserId) 
        {
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Set order complete status by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ServiceResponseStatus SetCompletedOrderStatus(int orderId)
        {
            return ServiceResponseStatus.Completed;
        }
        /// <summary>
        /// Set order InProcessing status by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ServiceResponseStatus SetInProcessingOrderStatus(int orderId) 
        {
            return ServiceResponseStatus.Completed;
        }
    }
}