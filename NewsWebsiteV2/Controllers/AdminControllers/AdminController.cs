using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.AdminControllers
{
    public class AdminController : Controller
    {
        private IDbConnection db;
        public AdminController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult AdminPage()
        {
            return View();
        }
        public Mess AdminLogin(string? userName, string? passWord) // Ajax Login 
        {
            try
            {
                var list = db.Query<LoginAndRegister>("select * from LoginAndRegister where username = @userName and password = @passWord and Role = 'Admin' ", new { userName = userName, passWord = passWord }); // tạo list từ dữ liệu lệnh SQL lên
                if (list == null || list.Count() == 0) return new Mess { checker = false, message = "Thất bại!", status = 2 }; // dữ liệu bị (null) hoặc k có dữ liệu. Trà sai tk false 
                var login2 = "tokenabcd"; // đặt tên cookie token
                CookieOptions options = new CookieOptions // Lệnh tự xóa tk sau 1 ngày
                {
                    Expires = DateTime.Now.AddSeconds(300) // Thời gian tồn tại.
                };
                string login_token = login2; // lưu biến vừa đăng nhập
                Response.Cookies.Append("login_token", login_token, options); // lưu đăng nhập, add vào cookie
            }
            catch (Exception e)
            {
                return new Mess { checker = false, message = e.Message, status = 2 }; // kiểm tra lần nữa check lỗi
            }
            return new Mess { checker = true, message = "Thành Công", status = 0 }; // Trả đúng tk true 
        }
        public IActionResult AdminLogout()
        {
            var login_token = ""; // cho tài khoản = rỗng

            CookieOptions options1 = new CookieOptions // Kiểm tra tài khoản rỗng
            {
                Expires = DateTime.Now.AddSeconds(1) // sau 1s sẽ logout Cookie
            };
            string login_token1 = login_token; // gán biến mới "rỗng" vào.
            Response.Cookies.Append("login_token", login_token1, options1); // gán lưu vào cookie
            return RedirectToAction("AdminPage");
        }
    }
}
