using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationForoNetCore.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "Over21")]
    [Authorize(Policy = "CanLogin")]
    public class HomeController : Controller
    {
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
