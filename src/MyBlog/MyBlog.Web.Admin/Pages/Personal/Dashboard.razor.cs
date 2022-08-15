using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Commands.Posts.Delete;
using MyBlog.Application.Enum;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Requests.Posts;
using MyBlog.Share.Wrappers;
using static MudBlazor.CategoryTypes;

namespace MyBlog.Web.Admin.Pages.Applications.Personal
{
    public partial class Dashboard 
    { 
        private  MudTable<PostForGetAllResponse> Table { get; set; }
        private PageList<PostForGetAllResponse> Posts { get; set; }
        private  QueryPostStatus Option { get; set; }
      
        public int BindPage { get; set; }
        private string searchString1 { get; set; } = "";
        [Inject]
        public IQueryBus QueryBus { get; set; }
        [Inject]
        public ICommandBus CommandBus { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [Inject]
        public NavigationManager Navigator { get; set; }
        public PostForGetAllResponse postSelected { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await QueryPostByPage(1);
        }
        private async Task QueryPost(GetAllPostQuery query)
        {
            var result = await QueryBus.Send<Result<PageList<PostForGetAllResponse>>>(query, default);
            Posts = result.Data;
        }
        private async Task SearchPost(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await QueryPostByPage();
            }
      
        }
        private void EditPost()
        {
            Navigator.NavigateTo($"/application/Post/{postSelected.Id}");
        }
        private async Task DeletePost()
        {
            DeletePostCommand delete = new DeletePostCommand(postSelected.Id);
         var result=  await CommandBus.Send<Result<Unit>>(delete, default);
       
            AddSnackBar(result.Messages[0]);
            await PageChanged(Posts.CurrentPage);

        }
        private void AddSnackBar(string message)
        {
            Snackbar.Add(message);
        }
        private async Task QueryPostByPage(int i=1)
        {
            GetAllPostQuery query = new GetAllPostQuery();
            query.PageSize = 5;
            query.PageNumer = i;
            query.SearchString = searchString1;
            query.Status = Option;
           await QueryPost(query);

        }
        private async Task PageChanged(int i)
        {
            await QueryPostByPage(i);
        }
     

    }
}
