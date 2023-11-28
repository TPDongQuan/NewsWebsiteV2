using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("Contact")]
    public class ContactModel
    {
        [Key]
        public int contactID { get; set; }
        public string contactMess { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}
