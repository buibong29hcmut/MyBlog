using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Application.Requests.Posts;
using MyBlog.Application.Requests.Tags;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Constants.Cache;
using MyBlog.Share.Wrappers;
using System.Text.Json;

namespace MyBlog.Web.App.Pages
{
    public partial class PostContent
    {

        [Parameter]
        public string Name { get; set; }
        [Inject]
        public IQueryBus Query { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IDistributedCache Cache { get; set; }
        private PostDetail? Post { get; set; }
        [Inject]
        private AppState _appState { get; set; }
        public async Task GetDetailPostAsync(string header)
        { 
            var cachedPostString = await Cache.GetStringAsync(header);
            if (cachedPostString == null)
            {
                GetPostDetailByNameQuery query = new GetPostDetailByNameQuery(header);
                var result = await Query.Send<Result<PostDetail>>(query, default);
                if(result.Data == null){
                    NavigationManager.NavigateTo("/");
                    return;
                }
                var postContent = JsonSerializer.Serialize(result.Data);
                Post = result.Data;
                await Cache.SetStringAsync(header, postContent);
                return;
            }
             Post = JsonSerializer.Deserialize<PostDetail>(cachedPostString);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (!String.IsNullOrEmpty(Name))
            {
                await GetDetailPostAsync(Name);
            }
            StateHasChanged();
            await base.OnAfterRenderAsync(firstRender);
            _appState.LoadingIsFinished = true;

        }


    }
}
