using Dapper.Contrib.Extensions;

namespace NewsWebsiteV2.Models
{
    [Table("LoginAndRegister")]
    public class LoginAndRegister
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
    public enum Gender
    {
        None,
        Male,
        Female
    }
    
}
