using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;
namespace NewsWebsiteV2.Controllers.Categories
{
    public class CategoriesController : Controller
    {
        private IDbConnection db;
        public CategoriesController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult Categories()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabc") return View("Login");
            var list = db.Query<Category>("select * from  Category").ToList();
            return View(list);
        }
    }
}
