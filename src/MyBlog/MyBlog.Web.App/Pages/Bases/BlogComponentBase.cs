
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Distributed;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Application.Requests.Tags;
using MyBlog.Share.Constants.Cache;
using MyBlog.Share.Wrappers;
using MyBlog.Web.App.SEOs;
using System.Text.Json;

namespace MyBlog.Web.App.Pages.Bases
{
    public class BlogComponentBase:ComponentBase
    {
        [Inject] protected IQueryBus QueryBus { get; set; }
        [Inject] protected IDistributedCache Cache { get; set; }
        [Inject] protected IConfiguration Configuration { get; set; }
        [Inject] protected AppState _appState { get; set; }
        [Inject] protected MetaDataState State { get; set; }

        protected bool Spiner = false;
        protected List<TagForResponse> Tags { get; private set; }
        protected SeoModel Model { get; set; }
        protected async Task GetAllTags()
        {
            var cachedTagString = await Cache.GetStringAsync(ApplicationConstant.cacheTag);
            if (string.IsNullOrEmpty(cachedTagString))
            {
                IQuery<Result<List<TagForResponse>>> query = new GetAllTagQuery();
                var result = await QueryBus.Send<Result<List<TagForResponse>>>(query, default);
                var tags = JsonSerializer.Serialize(result.Data);
                await Cache.SetStringAsync(ApplicationConstant.cacheTag, tags);
                Tags = result.Data;
                return;
            }
            var cachedTags = JsonSerializer.Deserialize<List<TagForResponse>>(cachedTagString);

            Tags = cachedTags;

     

        }
    }
}
