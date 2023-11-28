using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.LoginControllers
{
    public class LoginController : Controller
    {

        private IDbConnection db;
        public LoginController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult LoginPage()
        {
            return View();
        }
        public Mess Login(string? userName, string? passWord) // Ajax Login 
        {
            try
            {
                var list = db.Query<LoginAndRegister>("select * from LoginAndRegister where username = @userName and password = @passWord and Role = 'User' ", new { userName = userName, passWord = passWord }); // tạo list từ dữ liệu lệnh SQL lên
                if (list == null || list.Count() == 0) return new Mess { checker = false, message = "Thất bại!", status = 2 }; // dữ liệu bị (null) hoặc k có dữ liệu. Trà sai tk false 
                var login = "tokenabc"; // đặt tên cookie token
                CookieOptions options = new CookieOptions // Lệnh tự xóa tk sau 1 ngày
                {
                    Expires = DateTime.Now.AddSeconds(300) // Thời gian tồn tại.
                    
                };
                string login_token = login; // lưu biến vừa đăng nhập
                Response.Cookies.Append("login_token", login_token, options); // lưu đăng nhập, add vào cookie
            }
            catch (Exception e)
            {
                return new Mess { checker = false, message = e.Message, status = 2 }; // kiểm tra lần nữa check lỗi
            }
            return new Mess { checker = true, message = "Thành Công", status = 0 }; // Trả đúng tk true 
        }
        public IActionResult Logout()
        {
            var login_token =""; // cho tài khoản = rỗng

            CookieOptions options1 = new CookieOptions // Kiểm tra tài khoản rỗng
            {
                Expires = DateTime.Now.AddSeconds(1) // sau 1s sẽ logout Cookie
            };
            string login_token1 = login_token; // gán biến mới "rỗng" vào.
            Response.Cookies.Append("login_token", login_token1, options1); // gán lưu vào cookie
            return RedirectToActionPermanent("LoginPage"); // Giống View(). Nhưng chạy lại đúng Action.
        }
    }
}
/* Lỗi 1: không thể login vào lần nữa vì sẽ bị trả Login/HomePage/HomePage, phải là HomePage/HomePage
    
    Truyển dữ liệu gọi là Query String
 */