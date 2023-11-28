using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;


namespace NewsWebsiteV2.Services
{
    public class LoginServiceController : Controller
    {
        private IDbConnection db;
        public LoginServiceController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }

        public Mess Login(string? userName, string? passWord)
        {
            try
            {
                var list = db.Query<LoginAndRegister>("select * from AdminsLogin where Username = @userName and Password = @passWord", new { userName = userName, passWord = passWord }); // tạo list từ dữ liệu lệnh SQL lên
                if (list == null || list.Count() == 0) return new Mess { checker = false, message = "Thất bại!", status = 2 }; // dữ liệu bị (null) hoặc k có dữ liệu. Trà sai tk false 

                var login = "tokenabc"; // đặt tên cookie token
                CookieOptions options = new CookieOptions // Lệnh tự xóa tk sau 1 ngày
                {
                    Expires = DateTime.Now.AddSeconds(300) // Thời gian 1 ngày.
                };
                string login_token = login; // lưu biến vừa đăng nhập
                Response.Cookies.Append("login_token", login_token, options); // lưu đăng nhập
            }
            catch (Exception e)
            {
                return new Mess { checker = false, message = e.Message, status = 2 }; // kiểm tra lần nữa check lỗi
            }
            return new Mess { checker = true, message = "Thành Công", status = 0 }; // Trả đúng tk true 
        }
    }
}
