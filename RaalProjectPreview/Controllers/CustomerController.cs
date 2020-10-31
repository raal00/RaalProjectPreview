using System;
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
            return Json(null);
        }
        [Route("AddItemToCase")]
        public JsonResult AddItemToCase()
        {
            return Json(null);
        }
        [Route("CreateOrder")]
        public JsonResult CreateOrder()
        {
            return Json(null);
        }
        [Route("ShowMyOrders")]
        public JsonResult ShowMyOrders()
        {
            return Json(null);
        }
        [Route("CloseOrder")]
        public JsonResult CloseOrder()
        {
            return Json(null);
        }
    }
}