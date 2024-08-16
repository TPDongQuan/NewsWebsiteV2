using Dapper;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc;


namespace NewsWebsiteV2.Controllers.HomePageController
{
    public class HomePageController : Controller
    {
        private IDbConnection db;
        public HomePageController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        [Route("~/homepage")]
		public IActionResult HomePage()
        {
            var All = new AllLayout();
            var login_token = Request.Cookies["login_token"]; 
            if (login_token != "tokenabc") return RedirectToAction("Logout", "Login");
            var listnewsa = db.Query<ListNewsA>("select top 4 * from  ListNewsA ORDER BY listnewsAID DESC;").ToList(); // Tạo list cho top List News mới nhất. 
            All.ListNewsA = listnewsa; // viết services chung vào controller
            return View(All);
        }
    }
}
// Cần kết nối dữ liệu với database để dễ quản lý và gán id_HomePage cho các trang của mình.
