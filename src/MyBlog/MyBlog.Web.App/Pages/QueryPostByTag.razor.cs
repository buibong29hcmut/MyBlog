using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Requests.Posts;
using MyBlog.Share.Wrappers;
using MyBlog.Web.App.Components;
using System.Xml.Linq;

namespace MyBlog.Web.App.Pages
{
    public partial class QueryPostByTag
    {
        [Inject] private IQueryBus QueryBus { get; set; }
        [Parameter]
        public int Page { get; set; }= 1;
        [Parameter]
        public string ?TagName { get; set; }
        public string UrlFormat=> $"/posts/tag/{TagName}/?Page";
      
        [Inject] NavigationManager Navigator { get; set; }
        private PageList<PostForGetAllByTagName> PostList { get; set; }
        [Inject]
        private AppState _appState { get; set; }


        public async Task GetPostByTagName()
        {
            GetAllPostByTagQuery query = new GetAllPostByTagQuery()
            {
                TagName = this.TagName,
                PageNumer = Page,
                PageSize = 6,
            };
            var result = await QueryBus.Send<Result<PageList<PostForGetAllByTagName>>>(query);
            PostList = result.Data;

        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {

            parameters.SetParameterProperties(this);
            var uri = Navigator.ToAbsoluteUri(Navigator.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);

            if (queryStrings.TryGetValue("Page", out var pagenumber))
            {
                try
                {
                    Page = Convert.ToInt32(pagenumber);
                    if (Page < 0)
                    {
                        Page = 1;
                    }
                }
                catch
                {
                    Navigator.NavigateTo("");
                }
            }
            if (parameters.TryGetValue<string>(nameof(TagName), out var value))
            {
                if (value is not null)
                {
                    await GetPostByTagName();

                }
            }

            StateHasChanged();
            await base.SetParametersAsync(ParameterView.Empty);
            _appState.LoadingIsFinished = true;

        }


    }
}
