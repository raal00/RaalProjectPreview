using RaalProjectPreview.BLL.Models.Enums;
using RaalProjectPreview.BLL.Models.Home.Request;
using RaalProjectPreview.BLL.Models.Home.Response;
using RaalProjectPreview.BLL.Models.Home.ServiceModels.Response;
using RaalProjectPreview.BLL.Services;
using System.Web.Mvc;

namespace RaalProjectPreview.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Main panel
        /// </summary>
        /// <returns></returns>
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("About")]
        public ActionResult About()
        {
            return View();
        }
        [Route("Contact")]
        public ActionResult Contact()
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
        [ValidateAntiForgeryToken]
        public RedirectResult Login(LoginRequestModel request)
        {
            LoginResponseModel response = new LoginResponseModel();
            UserAuthService authService = new UserAuthService();
            ServiceLoginResponse status = authService.LoginUser(request);
            Session["Role"] = status.Role.ToString();
            if (status.Role == Security.Roles.ClientRole.Manager)
            {
                Session["Authed"] = true;
                return Redirect("/Admin");
            }
            else if (status.Role == Security.Roles.ClientRole.Customer)
            {
                Session["Authed"] = true;
                return Redirect("/Customer");
            }
            else
            {
                Session["Authed"] = false;
                return Redirect("/Home/Login");
            }
        }
        [Route("LogOut")]
        public RedirectResult LogOut()
        {
            Session["Authed"] = false;
            return Redirect("/Home/Login");
        }
        [Route("SignIn")]
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [Route("SignIn")]
        [HttpPost]
        public RedirectResult SignIn(AuthRequestModel request)
        {
            UserAuthService authService = new UserAuthService();
            ResponseStatus status = authService.SignInUser(request);
            if (status == ResponseStatus.Completed)
            {
                Session["Role"] = "Customer";
            }
            return Redirect("/Home/Login");
        }

        


    }
}