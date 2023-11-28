using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        public int newsID { get; set; }
        public string titles { get; set; }
        public string contents { get; set; }
        public string authors { get; set; }

        public int listnewsAID { get; set; }
        public DateTime DatePost { get; set; }
    }
}

