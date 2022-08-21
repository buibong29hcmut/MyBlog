namespace MyBlog.Web.Admin.Models
{
    public class UserDto
    {   public string UserId { get; set; }
        public string UserName { get; set; }
        public string ImageProfile { get; set; }
        public string Email { get; set; }
        public string LoginByIp { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
