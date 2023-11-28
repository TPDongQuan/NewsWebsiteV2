using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;
namespace NewsWebsiteV2.Controllers.About
{
    public class AboutController : Controller
    {
        private IDbConnection db;
        public AboutController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult About()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabc") return View("Login");
            var list = db.Query<AboutModel>("select * from  About").ToList();
            return View(list);
        }
    }
}
// bi loi