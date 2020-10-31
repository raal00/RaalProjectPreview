using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Request;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.BLL.Models.Home.ServiceModels.Response;
using RaalProjectPreview.BLL.Services;
using System.Web.Mvc;

namespace RaalProjectPreview.Controllers
{
    [Authorize]
    [Route("Home")]
    public class HomeController : Controller
    {
        [Route("Login")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.RetUrl = returnUrl;
            return View();
        }
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginRequestModel request, string returnUrl)
        {
            LoginResponseModel response = new LoginResponseModel();
            UserAuthService authService = new UserAuthService();
            ServiceLoginResponse status = authService.LoginUser(request);
            
            return Json(response);
        }

        [Route("SignIn")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [Route("SignIn")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(AuthRequestModel request)
        {
            AuthResponseModel response = new AuthResponseModel();
            UserAuthService authService = new UserAuthService();
            ResponseStatus status = authService.SignInUser(request);
            if (status == ResponseStatus.Completed)
            {

            }
            // call auth service
            return View(response);
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}