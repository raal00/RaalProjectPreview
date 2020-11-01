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
                response.Message = "";
                response.Items = new List<Item>();
                return Json(response);
            }
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
                response.Message = "Err";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            ResponseStatus status = customerService.AddItemToCase(request.ItemId, customerID);
            response.responseStatus = status;
            if (status != ResponseStatus.Completed)
                response.Message = "";
            else 
                response.Message = "";

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
                response.Message = "Err";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            Order order = customerService.CreateOrder(customerID);
            if (order == null)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "";
                return Json(response);
            }
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
                response.Message = "";
                return Json(response);
            }
            response.responseStatus = ResponseStatus.Completed;
            response.Message = "";
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
                response.Message = "Err";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            ResponseStatus status = customerService.RemoveOrder(request.OrderId, customerID);
            if (status == ResponseStatus.Failed)
            {
                response.responseStatus = ResponseStatus.Failed;
                response.Message = "";
                return Json(response);
            }
            response.responseStatus = ResponseStatus.Completed;
            response.Message = "";
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
                response.Message = "Err";
                return Json(response);
            }
            int customerID = (int)Session["ID"];
            response.CustomerCase = customerService.GetMyCase(customerID);
            return Json(response);
        }
    }
}