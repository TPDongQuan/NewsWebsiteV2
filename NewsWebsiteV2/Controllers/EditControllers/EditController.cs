using Microsoft.AspNetCore.Mvc;

namespace NewsWebsiteV2.Controllers.EditControllers
{
    public class EditController : Controller
    {
        public IActionResult EditPage()
        {
            var login_token = Request.Cookies["login_token"]; // gọi biến mới và kiểm tra xem tên nó đã có trong Cookie chưa
            if (login_token != "tokenabcd") return View("AdminLogin"); // Nếu chưa có thì trả về VIew cũ. 
            return View();
        }
    }
}
/*
 -Phải kiểm tra cookie cho mỗi trang đang nhập vào. Cần trả về View của controller khác.
 */