using Microsoft.AspNetCore.Mvc;


namespace NewsWebsiteV2.Controllers.HomePageController
{
    public class HomePageController : Controller
    {
        public IActionResult HomePage()
        {
            var login_token = Request.Cookies["login_token"]; 
            if (login_token != "tokenabc") return View("Login");
            return View();
        }
    }
}
// Cần kết nối dữ liệu với database để dễ quản lý và gán id_HomePage cho các trang của mình.
