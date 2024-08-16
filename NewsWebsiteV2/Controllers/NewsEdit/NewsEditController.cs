using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.CateEdit
{
    public class NewsEditController : Controller
    {
        private IDbConnection db;
        public NewsEditController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult NewsEditPage()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("Login");
            var list = db.Query<News>("select * from News order by newsID asc").ToList();
            return View(list);
        }
        public IActionResult Update(int newsID) //Mở trang edit: Lấy khóa chính lên trước để xác định thông tin:
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            var model1 = db.QueryFirstOrDefault<News>("select * from News where newsID = @newsID", param: new { newsID = newsID }); // Hiển thị ID đã có sãn
            return View(model1); // Trả đó ID ra
        }

        [HttpPost]
        public IActionResult SubUpdate(News model) // Hành động edit: Sau khi xác định, tiến hành sửa thông tin:
        {
            try
            {
                var model1 = db.QueryFirstOrDefault<News>("select * from News where newsID = @newsID", param: new { newsID = model.newsID }); // tạo biến mới để xác định thông tin. gọi khóa chính lên
                model1.titles = model.titles; // đổi, gán biến mới vào
                model1.contents = model.contents; // đổi, gán biến mới vào 
                model1.authors = model.authors; // đổi, gán biến mới vào
                model1.listnewsAID = model.listnewsAID;
                model1.DatePost = model.DatePost;
                var it = db.Update(model1);// hành động dapper lưu biến mới.
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            
            
        }
        public IActionResult Delete(News model) // gọi tất cả mục và giá trị ra để xóa hết
        {
            // lệnh xóa
            try
            {
                var it = db.Delete(model);
            }
            catch (Exception ex)
            {
                var i = ex;
            }
            return View("ThanhCong");
        }
        public IActionResult ThanhCong() // Trang thông báo
        {
            return View(); // Trả ngươc lại view chính
        }
        public IActionResult Create(News model) // gọi lên trang create
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            return View(); //trả view
        }

        [HttpPost]
        public IActionResult SubCreate(News model) //thi hành lệnh insert mới vào
        {
            try
            {
                var it = db.Insert(model); // hành động insert
                return Json(true);
            }
            catch (Exception ex)
            {
                var i = ex;
                return Json(false);
            }
        }
    }
}
/*Chưa có view cho create tin tức mới*/