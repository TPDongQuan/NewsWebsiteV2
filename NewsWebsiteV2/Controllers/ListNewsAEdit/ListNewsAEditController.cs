using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.ListNewsAEdit
{
    public class ListNewsAEditController : Controller
    {
        private IDbConnection db;

        public ListNewsAEditController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult ListNewsAEditPage()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            var list = db.Query<ListNewsA>("select * from  ListNewsA").ToList();
            return View(list);
        }
        public IActionResult Update(int listnewsAID) //Mở trang edit: Lấy khóa chính lên trước để xác định thông tin:
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            var model1 = db.QueryFirstOrDefault<ListNewsA>("select * from ListNewsA where listnewsAID = @listnewsAID", param: new { listnewsAID = listnewsAID }); // Hiển thị ID đã có sãn

            return View(model1); // Trả đó ID ra
        }
        public IActionResult SubUpdate(ListNewsA model) // Hành động edit: Sau khi xác định, tiến hành sửa thông tin:
        {
            var model1 = db.QueryFirstOrDefault<ListNewsA>("select * from ListNewsA where listnewsAID = @listnewsAID", param: new { listnewsAID = model.listnewsAID  }); // tạo biến mới để xác định thông tin. gọi khóa chính lên
            model1.listnewsA = model.listnewsA; // đổi, gán biến mới vào 
            model1.cateID = model.cateID;
            var it = db.Update(model1);// lưu biến mới.
            return View("ThanhCong");
        }
        public IActionResult Delete(ListNewsA model) // gọi tất cả mục và giá trị ra để xóa hết
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
        public IActionResult Create(ListNewsA model) // gọi lên trang create
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            return View(); //trả view
        }
        public IActionResult SubCreate(ListNewsA model) //thi hành lệnh insert mới vào
        {
            try
            {
                var it = db.Insert(model); // hành động insert
            }
            catch (Exception ex)
            {
                var i = ex;
            }
            return View("ThanhCong");

       }
    }
}
