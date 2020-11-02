using RaalProjectPreview.BLL.Models.Customer.Request;
using RaalProjectPreview.BLL.Models.Customer.Response;
using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Services;
using RaalProjectPreview.DAL.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace RaalProjectPreview.Controllers
{
    [Route("Customer")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Customer panel
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("GetItemList")]
        public JsonResult GetItemList()
        {
            GetItemListResponseModel response = new GetItemListResponseModel();
            CustomerService customerService = new CustomerService();
            response.Items = customerService.GetItemList();
            if (response.Items == null) 
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "No items in shop";
                response.Items = new List<Item>();
                return Json(response);
            }
            response.responseStatus = ResponseStatus.Completed;
            response.Message = "Succsessful";
            return Json(response);
        }
        [Route("AddItemToCase")]
        public JsonResult AddItemToCase(AddItemToCaseRequestModel request)
        {
            AddItemToCaseResponseModel response = new AddItemToCaseResponseModel();
            CustomerService customerService = new CustomerService();
            if (Session["ID"] == null)
            {
                response.responseStatus = BLL.Models.Enums.ResponseStatus.Failed;
                response.Message = "Unauthorized user";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            ResponseStatus status = customerService.AddItemToCase(request.ItemId, customerID);
            response.responseStatus = status;
            if (status != ResponseStatus.Completed)
                response.Message = "New Item was added";
            else 
                response.Message = "Unable to add new item";

            return Json(response);
        }
        [Route("CreateOrder")]
        public JsonResult CreateOrder()
        {
            CreateOrderResponseModel response = new CreateOrderResponseModel();
            CustomerService customerService = new CustomerService();
            if (Session["ID"] == null)
            {
                response.responseStatus = BLL.Models.Enums.ResponseStatus.Failed;
                response.Message = "Unauthorized user";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            ResponseStatus status = customerService.CreateOrder(customerID);
            if (status == ResponseStatus.Failed)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "Unable to find selected order";
                return Json(response);
            }
            response.Message = "New order was created";
            response.responseStatus = ResponseStatus.Completed;
            return Json(response);
        }
        [Route("ShowMyOrders")]
        public JsonResult ShowMyOrders()
        {
            ShowMyOrdersResponseModel response = new ShowMyOrdersResponseModel();
            CustomerService customerService = new CustomerService();
            if (Session["ID"] == null)
            {
                response.responseStatus = BLL.Models.Enums.ResponseStatus.Failed;
                response.Message = "Err";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            List<Order> orders = customerService.GetCustomerOrders(customerID);
            if (orders == null)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "No orders";
                return Json(response);
            }
            response.responseStatus = ResponseStatus.Completed;
            response.Message = "Succsessful";
            response.Orders = orders;
            return Json(response);
        }
        [Route("CloseOrder")]
        public JsonResult CloseOrder(CloseOrderRequestModel request)
        {
            CloseOrderResponseModel response = new CloseOrderResponseModel();
            CustomerService customerService = new CustomerService();
            if (Session["ID"] == null)
            {
                response.responseStatus = BLL.Models.Enums.ResponseStatus.Failed;
                response.Message = "Unauthorized user";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            ResponseStatus status = customerService.RemoveOrder(request.OrderId, customerID);
            if (status == ResponseStatus.Failed)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "Unable to close selected order";
                return Json(response);
            }
            response.responseStatus = ResponseStatus.Completed;
            response.Message = "Order was closed";
            return Json(response);
        }
        [Route("MyCase")]
        public JsonResult MyCase()
        {
            MyCaseResponseModel response = new MyCaseResponseModel();
            CustomerService customerService = new CustomerService();
            if (Session["ID"] == null)
            {
                response.responseStatus = BLL.Models.Enums.ResponseStatus.Failed;
                response.Message = "Unauthorized user";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            response.CustomerCase = customerService.GetMyCase(customerID);
            if (response.CustomerCase == null)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "No case";
                return Json(response);
            }
            response.responseStatus = ResponseStatus.Completed;
            response.Message = "Succsessful";
            return Json(response);
        }
    }
}