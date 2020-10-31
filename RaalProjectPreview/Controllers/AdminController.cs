using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaalProjectPreview.Controllers
{
    [Authorize]
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
            return View();
        }


        [Route("AddItemToShop")]
        public JsonResult AddItemToShop()
        {
            return Json(null);
        }
        [Route("DeleteItemFromShop")]
        public JsonResult DeleteItemFromShop()
        {
            return Json(null);
        }
        [Route("EditItemInShop")]
        public JsonResult EditItemInShop()
        {
            return Json(null);
        }


        [Route("AddNewUser")]
        public JsonResult AddNewUser()
        {
            return Json(null);
        }
        [Route("EditUser")]
        public JsonResult EditUser()
        {
            return Json(null);
        }
        [Route("DeleteUser")]
        public JsonResult DeleteUser()
        {
            return Json(null);
        }


        [Route("SetCompletedOrderStatus")]
        public JsonResult SetCompletedOrderStatus()
        {
            return Json(null);
        }
        [Route("SetInProcessingOrderStatus")]
        public JsonResult SetInProcessingOrderStatus()
        {
            return Json(null);
        }
    }
}