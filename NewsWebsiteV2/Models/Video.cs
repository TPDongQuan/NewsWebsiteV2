using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("Video")]
    public class Video
    {
        [Key]
        public int Video_Id { get; set; }
        public string Video_Name { get; set; }
        public string Video_Link { get; set; }
        public string Video_SmallImg { get; set; }
    }
}
