using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Requests.Posts;
using MyBlog.Share.Wrappers;
using MyBlog.Web.App.Pages.Bases;
using System.Xml.Linq;

namespace MyBlog.Web.App.Pages
{
    public partial class Index
    {
        [Inject] private IQueryBus QueryBus { get; set; }
        [Inject] private IConfiguration Configuration { get; set; } 
        [Parameter]
        public int Page { get; set; } = 1;
        [Parameter]
        public string TagName { get; set; }
        [Inject]
        private AppState _appState { get; set; }

        [Inject] NavigationManager Navigator { get; set; }
        private PageList<PostForGetAllResponse> PostList { get; set; } 

        public string BaseUrlAdmin => Configuration.GetValue<string>("BaseAddressAdmin");
    
        public string UrlFormat =>  $"/posts/?Page";


        public async Task GetPostByPage()
        {
            GetAllPostQuery query = new GetAllPostQuery()
            {
                PageNumer = Page
            };
            var result = await QueryBus.Send<Result<PageList<PostForGetAllResponse>>>(query, default);
            PostList = result.Data;
        }
   
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                var uri = Navigator.ToAbsoluteUri(Navigator.Uri);
                var queryStrings = QueryHelpers.ParseQuery(uri.Query);
                if (queryStrings.TryGetValue("Page", out var PageNumber))
                {
                    try
                    {
                        this.Page = Convert.ToInt32(PageNumber);
                        if (this.Page < 0)
                        {
                            Page = 1;
                        }
                    }
                    catch
                    {
                        Navigator.NavigateTo("/");
                    }
                }
                await GetPostByPage();
              
            }
            await base.OnAfterRenderAsync(firstRender);
            StateHasChanged();
            _appState.LoadingIsFinished = true;


        }



    }
}
