using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Services
{
    public class RegisterServiceController : Controller
    {
        private IDbConnection db;
        public RegisterServiceController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult Register(LoginAndRegister model) //, bool? flag) // model: là gán biến tổng cho tất cả biến có trong UsersRegisterModel
        {
            if (model.username == null) return View(); // kiểm tra không đc null 
            var list = db.Query<LoginAndRegister>("select * from LoginAndRegister where username = @userName ", new { userName = model.username }).ToList(); // lấy list từ SQL lên
            if (list.Count != 0) return View();// kiểm tra nếu tk có chưa. Nếu có r thì trả view cũ 
            
            /*if (flag == true)
                model.Role = "User";
            else
                model.Role = "Admin";
            */

            //var insert = db.Execute("insert into login (usename  ) values (@name)", new { Name = model.Name });
            var it = db.Insert(model); // Dapper insert: thực hiện chức năng insert nhập vào bảng SQL khi đăng ký.
                                       //var it1 = db.Update(model);
            var login2 = "tokenabb"; // đặt tên (nickname ảo lưu trên Cookie) của người dùng nên là login2.
            CookieOptions options = new CookieOptions // Lệnh tự xóa tk sau 1 ngày
            {
                Expires = DateTime.Now.AddSeconds(60) // Thời gian tùy chỉnh.
            };
            string login_token = login2; // lưu biến vừa đăng ký
            Response.Cookies.Append("login_token", login_token, options); // lưu đăng ký đó

            /*if (flag == true)
                return View("HomePage");
            else
                return View();
            */
            return View("HomePage");
        }
        /*public IActionResult RegisterAdmin(LoginAndRegister model) // model: là gán biến tổng cho tất cả biến có trong UsersRegisterModel
        {
            if (model.username == null) return View(); // kiểm tra không đc null 
            var list = db.Query<LoginAndRegister>("select * from LoginAndRegister where username = @userName ", new { userName = model.username }).ToList(); // lấy list từ SQL lên
            if (list.Count != 0) return View();// kiểm tra nếu tk có chưa. Nếu có r thì trả view cũ 
            model.Role = "";
            //var insert = db.Execute("insert into login (usename  ) values (@name)", new { Name = model.Name });
            var it = db.Insert(model); // Dapper insert: thực hiện chức năng insert nhập vào bảng SQL khi đăng ký.
                                       //var it1 = db.Update(model);
            var login2 = "tokenabb"; // đặt tên (nickname ảo lưu trên Cookie) của người dùng nên là login2.
            CookieOptions options = new CookieOptions // Lệnh tự xóa tk sau 1 ngày
            {
                Expires = DateTime.Now.AddSeconds(60) // Thời gian tùy chỉnh.
            };
            string login_token = login2; // lưu biến vừa đăng ký
            Response.Cookies.Append("login_token", login_token, options); // lưu đăng ký đó
            return View("HomePage");
        } */
    }
}
