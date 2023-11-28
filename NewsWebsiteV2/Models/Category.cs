using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int cateID { get; set; }
        public string categories { get; set; }
        public string bkgimg { get; set; }
    }
}
