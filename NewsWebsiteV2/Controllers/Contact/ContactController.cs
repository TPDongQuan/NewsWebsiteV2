using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;
namespace NewsWebsiteV2.Controllers.Contact
{
    public class ContactController : Controller
    {
        private IDbConnection db;
        public ContactController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult Contact(ContactModel model)
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabc") return RedirectToAction("Logout", "Login");
            return View();
        }
        public IActionResult SubContact(ContactModel model) //thi hành lệnh insert mới vào
        {
            try
            {
                var it = db.Insert(model); // hành động insert
            }
            catch (Exception ex)
            {
                var i = ex;
            }
            return View("Contact");

        }
    }
}
//Note: Contact Model cần liên kết với LoginAndRegister Model để lấy cùng Name ( hoặc kết hợp 2 bảng thành 1 - contact sẽ là 1 col trong LAR)
//Tạo thêm trang quản lý cho contact.