using RaalProjectPreview.BLL.Models.Home.Request;
using RaalProjectPreview.BLL.Models.Home.Response;
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
        public ActionResult Login(LoginRequestModel request, string returnUrl)
        {
            LoginResponseModel response = new LoginResponseModel();
            // call login service
            return View(response);
        }

        [Route("Auth")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Auth()
        {
            return View();
        }
        [Route("Auth")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Auth(AuthRequestModel request)
        {
            AuthResponseModel response = new AuthResponseModel();
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