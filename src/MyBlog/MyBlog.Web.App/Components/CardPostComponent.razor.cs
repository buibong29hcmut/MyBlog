using Microsoft.AspNetCore.Components;
using MyBlog.Application.Reponses.Posts;

namespace MyBlog.Web.App.Components
{
    public partial class CardPostComponent
    {
        [Parameter]
        public PostForGetAllResponse PostItem { get; set; }
        [Inject]
        private IConfiguration Configuration { get; set; }
        public string BaseUrlAdmin
        {
            get
            {
                return Configuration.GetValue<string>("BaseAddressAdmin");
            }
        }
        public string UrlImage
        {
            get
            {
                return BaseUrlAdmin + PostItem.ImagePost;
            }
        }

    }
}
