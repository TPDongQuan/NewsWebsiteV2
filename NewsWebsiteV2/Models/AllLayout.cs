using NewsWebsiteV2.Models;

namespace NewsWebsiteV2.Models
{
    public class AllLayout //Đây là 1 model xài chung nhieu models khac de tương tác với nhieu View 1 luc
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>(); // Dạng list
        public IEnumerable<ListNewsA> ListNewsA { get; set; } = new List<ListNewsA>();
        public IEnumerable<Video> Video { get; set; } = new List<Video>();
        public IEnumerable<Video> TopVideo { get; set; } = new List<Video>(); // tạo thêm 1 list dành cho top video.
    }
}
