using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.UserEdit
{
    public class UserEditController : Controller
    {
        private IDbConnection db;
        public UserEditController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult UserEditPage()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("Login");
            var list = db.Query<LoginAndRegister>("select * from  LoginAndRegister where Role = 'User'").ToList(); // Call table from dt 
            return View(list);
        }
        
    }
}
/*
 - Gọi bảng table và trang edit
 */