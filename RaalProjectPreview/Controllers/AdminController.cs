using RaalProjectPreview.BLL.Models.Admin.Request;
using RaalProjectPreview.BLL.Models.Admin.Response;
using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Services;
using RaalProjectPreview.DAL.Models;
using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaalProjectPreview.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Admin panel
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        public ActionResult Index()
        {
            if (Session["ID"] == null) return new RedirectResult("/Home/Login");
            int customerID = (int)Session["ID"];
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            if (adminService.IsAdmin(customerID)) { 
                return View(); 
            }
            else {
                Session["ID"] = null;
                return new RedirectResult("/Home/Login");
            }
        }

        [Route("ShowOrders")]
        public JsonResult ShowOrders()
        {
            ShowOrdersResponseModel response = new ShowOrdersResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            response.Orders = adminService.ShowOrders().Select(x=>new BLL.Models.Admin.OrderData() { 
                 Id = x.Id,
                 OrderDate = x.OrderDate.ToShortDateString(),
                 CustomerId = x.CustomerId,
                 OrderNumber = x.OrderNumber,
                 ShipmentDate = x.ShipmentDate.ToShortDateString(),
                 Status = x.Status
            }).ToList();
            return Json(response);
        }
        [Route("ShowUsers")]
        public JsonResult ShowUsers()
        {
            ShowUserListResponseModel response = new ShowUserListResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            response.UserList = adminService.ShowUsers();
            return Json(response);
        }
        [Route("ShowItems")]
        public JsonResult ShowItems()
        {
            ShowItemsResponseModel response = new ShowItemsResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            response.Items = adminService.ShowItems();
            return Json(response);
        }



        [Route("AddItemToShop")]
        public JsonResult AddItemToShop(AddNewItemRequestModel request)
        {
            AddNewItemResponseModel response = new AddNewItemResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.AddNewItem(request.Item);

            if (status == ServiceResponseStatus.Completed) 
            {
                response.Message = "New item was added";
                response.responseStatus = ResponseStatus.Completed; 
            }
            else 
            {
                response.Message = "Unable to add new item";
                response.responseStatus = ResponseStatus.Failed; 
            }
            return Json(response);
        }
        [Route("DeleteItemFromShop")]
        public JsonResult DeleteItemFromShop(DeleteItemFromShopRequestModel request)
        {
            DeleteItemFromShopResponseModel response = new DeleteItemFromShopResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.DeleteItemFromShop(request.ItemId);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to delete selected item";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }
        [Route("EditItemInShop")]
        public JsonResult EditItemInShop(EditItemInShopRequestModel request)
        {
            EditItemInShopResponseModel response = new EditItemInShopResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.EditItemInShop(request.Item);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to edit selected item";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }


        [Route("AddNewUser")]
        public JsonResult AddNewUser(AddNewUserRequestModel request)
        {
            AddNewUserResponseModel response = new AddNewUserResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.AddNewUser(request.AllUserData.Customer, 
                                                                   request.AllUserData.AuthUserData,
                                                                   request.AllUserData.UserRole);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to add new user";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }
        [Route("EditUser")]
        public JsonResult EditUser(EditUserRequestModel request)
        {
            EditUserResponseModel response = new EditUserResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.EditUser(request.AllUserData.Customer, 
                                                                 request.AllUserData.AuthUserData, 
                                                                 request.AllUserData.UserRole);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to edit selected user";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }
        [Route("DeleteUser")]
        public JsonResult DeleteUser(DeleteUserRequestModel request)
        {
            DeleteUserResponseModel response = new DeleteUserResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.DeleteUser(request.UserId);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to delete selected user";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }


        [Route("SetCompletedOrderStatus")]
        public JsonResult SetCompletedOrderStatus(SetCompletedStatusRequestModel request)
        {
            SetCompletedStatusResponseModel response = new SetCompletedStatusResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.SetCompletedOrderStatus(request.OrderId);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to set completed status for selected order";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }
        [Route("SetInProcessingOrderStatus")]
        public JsonResult SetInProcessingOrderStatus(SetInProcessingStatusRequestModel request)
        {
            SetInProcessingResponseModel response = new SetInProcessingResponseModel();
            AdminService adminService = new AdminService(ApplicationContext.GetInstance());
            ServiceResponseStatus status = adminService.SetInProcessingOrderStatus(request.OrderId);

            if (status == ServiceResponseStatus.Completed)
            {
                response.Message = "Completed";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Message = "Unable to set inProcessing status for selected order";
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }
    }
}