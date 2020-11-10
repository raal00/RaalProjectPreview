using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Request;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.BLL.Models.Home.ServiceModels.Response;
using RaalProjectPreview.BLL.Services;
using RaalProjectPreview.DAL.Models;
using System.Web.Mvc;

namespace RaalProjectPreview.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        [Authorize]
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public JsonResult Login(LoginRequestModel request)
        {
            LoginResponseModel response = new LoginResponseModel();
            UserAuthService authService = new UserAuthService(ApplicationContext.GetInstance());
            ServiceLoginResponse status = authService.LoginUser(request);
            Session["ID"] = status.UserId;
            Session["Role"] = status.Role.ToString();
            if (status.Role == Security.Roles.ClientRole.Manager)
            {
                Session["Authed"] = true;
                response.Role = Security.Roles.ClientRole.Manager;
                response.responseStatus = ResponseStatus.Completed;
                response.Message = $"Привет менеджер {status.Name}!";
            }
            else if (status.Role == Security.Roles.ClientRole.Customer)
            {
                Session["Authed"] = true;
                response.Role = Security.Roles.ClientRole.Customer;
                response.Message = $"Привет пользователь {status.Name}!";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.Role = Security.Roles.ClientRole.Unauthorized;
                response.Message = "Неверный логин и/или пароль";
                response.responseStatus = ResponseStatus.Failed;
                Session["Authed"] = false;
            }
            return Json(response);
        }
        [Route("SignIn")]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [Route("SignIn")]
        [HttpPost]
        public JsonResult SignIn(AuthRequestModel request)
        {
            UserAuthService authService = new UserAuthService(ApplicationContext.GetInstance());
            AuthResponseModel response = new AuthResponseModel();
            ResponseStatus status = authService.SignInUser(request);
            if (status == ResponseStatus.Completed)
            {
                Session["Role"] = "Customer";
                response.responseStatus = ResponseStatus.Completed;
            }
            else
            {
                response.responseStatus = ResponseStatus.Failed;
            }
            return Json(response);
        }
    }
}