using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Application.Requests.Tags;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Constants.Cache;
using MyBlog.Share.Wrappers;
using System.Text.Json;

namespace MyBlog.Web.App.Components
{
    public partial class TagComponent
    {

 
        [Parameter] public List<TagForResponse> Tags { get; set; } 
        //public async Task GetAllTags()
        //{
        //    //var cachedTagString = await Cache.GetStringAsync(ApplicationConstant.cacheTag);
        //    //if (string.IsNullOrEmpty( cachedTagString))
        //    //{
        //    //    IQuery<Result<List<TagForResponse>>> query = new GetAllTagQuery();
        //    //    var result = await QueryBus.Send<Result<List<TagForResponse>>>(query, default);
        //    //    var tags = JsonSerializer.Serialize(result.Data);
        //    //    await Cache.SetStringAsync(ApplicationConstant.cacheTag, tags);
        //    //    Tags = result.Data;
        //    //    return;
        //    //}
        //    //var cachedTags = JsonSerializer.Deserialize<List<TagForResponse>>(cachedTagString);

        //    //Tags = cachedTags;

        //    IQuery<Result<List<TagForResponse>>> query = new GetAllTagQuery();
        //    var result = await QueryBus.Send<Result<List<TagForResponse>>>(query, default);
             
            
             
            
        

        //    Tags = result.Data;

        //}
        // protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        await GetAllTags();
        //        StateHasChanged();
        //    }
        //}
     

        private string ClassHover { get; set; }
        [Parameter] public string BackGroundColor { get; set; }
        [Parameter] public string HoverBackGroundColor { get; set; }
    }
}
