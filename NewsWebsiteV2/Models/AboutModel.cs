using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("About")]
    public class AboutModel
    {
        [Key]
        public int aboutID { get; set; }
        public string aboutContent { get; set; }
        public string aboutTitle { get; set; }
    }
}
