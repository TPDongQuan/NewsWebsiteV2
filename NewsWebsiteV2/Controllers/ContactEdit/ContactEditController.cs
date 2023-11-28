using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.UserEdit
{
    public class ContactEditController : Controller
    {
        private IDbConnection db;
        public ContactEditController (IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult ContactEditPage()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("Login");
            var list = db.Query<ContactModel>("select * from  Contact").ToList(); // Call table from dt 
            return View(list);
        }

    }
}
