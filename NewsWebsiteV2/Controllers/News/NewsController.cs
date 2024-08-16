using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;
namespace NewsWebsiteV2.Controllers.Categories
{
    public class NewsController : Controller
    {
        private IDbConnection db;
        public NewsController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult News(int? listnewsAID = 0)
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabc") return RedirectToAction("Logout", "Login");
            var list = db.Query<News>("select * from News where listnewsAID = @listnewsAID ", new { listnewsAID = listnewsAID }).ToList();
            return View(list);
        }
    }
}
