using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.RegisterControllers
{
    // # private ... 
    public class RegisterController : Controller
    {
        private IDbConnection db;
        public RegisterController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult RegisterPage()
        {
            return View();
        }
        public IActionResult Register(LoginAndRegister model) // model: là gán tổng các tất cả biến có trong view = model (UsersRegisterModel)
        {

            if (model.username == null) return View(); // kiểm tra không đc null 
            var list = db.Query<LoginAndRegister>("select * from LoginAndRegister where username = @userName ", new { userName = model.username }).ToList(); // lấy list từ SQL lên
            if (list.Count != 0) return View();// kiểm tra nếu tk có chưa. Nếu có r thì trả view cũ 
            model.Role = "User"; // tự động điền vào dt.
                                    //var insert = db.Execute("insert into login (usename ) values (@name)", new { Name = model.Name });
            var it = db.Insert(model); // Dapper insert: thực hiện chức năng insert nhập vào bảng SQL khi đăng ký.
                                        //var it1 = db.Update(model);
            var login = "tokenabcd"; // đặt tên (nickname ảo lưu trên Cookie) của người dùng nên là login2.
            CookieOptions options = new CookieOptions // Lệnh tự xóa tk sau 1 ngày
            {
                Expires = DateTime.Now.AddSeconds(600) // Thời gian tùy chỉnh.
            };
            string login_token = login; // lưu biến vừa đăng ký
            Response.Cookies.Append("login_token", login_token, options); // lưu đăng ký đó
            return RedirectToActionPermanent("LoginPage", "Login"); // sau khi đk Đúng
        }
    }
}
/*Task4: cai ajax cho register view
 */
