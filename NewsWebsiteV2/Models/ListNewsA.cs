using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("ListNewsA")]
    public class ListNewsA
    {
        [Key]
        public int listnewsAID { get; set; }
        public string listnewsA { get; set; }
        public int cateID { get; set; }
        public string bkgimg { get; set; }
        public string listnewsAShortDes { get; set; }
        public string Video_Link {  get; set; }
    }
}
// Thêm ảnh cho từng id tương ứng