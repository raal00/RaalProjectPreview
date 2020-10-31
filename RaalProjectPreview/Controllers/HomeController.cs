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
        public ActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginRequestModel request)
        {
            LoginResponseModel response = new LoginResponseModel();
            UserAuthService authService = new UserAuthService();
            ServiceLoginResponse status = authService.LoginUser(request);
            Session["Role"] = status.Role.ToString();
            
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
        public JsonResult SignIn(AuthRequestModel request)
        {
            AuthResponseModel response = new AuthResponseModel();
            UserAuthService authService = new UserAuthService();
            ResponseStatus status = authService.SignInUser(request);
            if (status == ResponseStatus.Completed)
            {
                Session["Role"] = "Customer";
            }
            return Json(response);
        }

        [AllowAnonymous]
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