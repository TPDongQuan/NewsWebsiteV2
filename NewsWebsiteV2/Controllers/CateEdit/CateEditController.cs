using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsWebsiteV2.Models;
using System.Data;

namespace NewsWebsiteV2.Controllers.CateEdit
{
    public class CateEditController : Controller
    {
        private IDbConnection db;
        public CateEditController(IConfiguration configuration)
        {
            db = new SqlConnection(configuration.GetConnectionString("DbConn"));
        }
        public IActionResult CateEditPage()
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            var list = db.Query<Category>("select * from  Category order by cateID asc").ToList();
            return View(list);
        }
        public IActionResult Update(int cateID) //Mở trang edit: Lấy khóa chính lên trước để xác định thông tin:
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            var model1 = db.QueryFirstOrDefault<Category>("select * from Category where cateID = @cateID", param: new { cateID = cateID }); // Hiển thị ID đã có sãn

            return View(model1); // Trả đó ID ra
        }
        public IActionResult SubUpdate(Category model) // Hành động edit: Sau khi xác định, tiến hành sửa thông tin:
        {
            var model1 = db.QueryFirstOrDefault<Category>("select * from Category where cateID = @cateID", param: new { cateID = model.cateID }); // tạo biến mới để thay thế nd. gọi khóa chính lên
            model1.categories = model.categories; // đổi, gán biến mới vào 
            var it = db.Update(model1);// lưu biến mới.
            return View("ThanhCong");
        }
        public IActionResult Delete(Category model) // gọi tất cả mục và giá trị ra để xóa hết
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
            return View("CateEditPage");
        }
        public IActionResult ThanhCong()
        {
            return View();
        }
        public IActionResult Create() // gọi lên trang create
        {
            var login_token = Request.Cookies["login_token"];
            if (login_token != "tokenabcd") return View("AdminLogin");
            return View(); //trả view
        }
        public IActionResult SubCreate(Category model) //thi hành lệnh insert mới vào
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