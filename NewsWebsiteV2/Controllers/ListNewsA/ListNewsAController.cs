using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;
namespace NewsWebsiteV2.Controllers.Categories
{
    public class ListNewsAController : Controller
    {
        private IDbConnection db;
        public ListNewsAController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }

        [Route("~/ListNewsA")]

        // Nên ktra kĩ: return Logout
        public IActionResult ListNewsA(int? cateID = 0 )
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabc") return RedirectToAction("Logout", "Login");
            var list = db.Query<ListNewsA>("select * from  ListNewsA where cateID = @cateID", new { cateID = cateID}).ToList(); //tạo list 
            return View(list);
        }
    }
}
/*
 Gọi đã lưu cateID (khi vào trang Category) lên tạo liên kết giữa 2 bảng ListNewsA và Category.
 Từ đó lấy dữ liệu từ bảng Category lên xài cho bảng ListNewsA
 */