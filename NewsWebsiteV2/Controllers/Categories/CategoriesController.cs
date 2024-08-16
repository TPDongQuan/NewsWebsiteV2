using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using NewsWebsiteV2.Models.ViewModel;
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

        [Route("~/Categories")]
        public IActionResult Categories()
        {
            var All = new AllLayout();

            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabc") return RedirectToAction("Logout","Login");

            var cate = db.Query<Category>("select * from  Category").ToList(); //tạo list cho Categories
            var listnewsa = db.Query<ListNewsA>("select top 3 * from  ListNewsA ORDER BY listnewsAID DESC;").ToList(); // Tạo list cho top List News mới nhất. 
            var video = db.Query<Video>("select * from  Video").ToList();
            var topvideo = db.Query<Video>("SELECT TOP 1 * FROM Video ORDER BY Video_Id DESC;").ToList();

            All.Categories = cate;
            All.ListNewsA = listnewsa; // viết services chung vào controller
            All.TopVideo = topvideo;
            All.Video = video;

            return View(All); // chèn nhiều view model vào được
        }
    }
}
