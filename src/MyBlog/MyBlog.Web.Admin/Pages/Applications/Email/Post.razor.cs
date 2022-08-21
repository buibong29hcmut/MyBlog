using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using MudBlazor;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Application.Requests.Posts;
using MyBlog.Application.Requests.Tags;
using MyBlog.Share.Wrappers;
using MyBlog.Web.Admin.Data;
using MyBlog.Web.Admin.Shared;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace MyBlog.Web.Admin.Pages.Applications.Email
{
    public partial class Post
    {
        [Inject] private IQueryBus QueryBus { get; set; }
        [Inject] private ICommandBus CommandBus { get; set; }
        [Inject] private NavigationManager Navigator { get; set; }
        [Inject] private IJSRuntime _jsRuntime { get; set; }
        [Parameter] public string? Id { get; set; }
        private EditorComponent editor { get; set; }
        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private string UrlSaveToDataBase { get; set; }
        private string UrlImagePost { get; set; }
        public bool Basic_CheckBox1 { get; set; }
        public CreateOrUpdatePostCommand PostItem { get; set; }
        EditorOptions editorOptions;
        string editorValue { get; set; } = "I hope you see me :)";
        private PostDetail PostDetail { get; set; }
        private HashSet<string> Tags { get; set; } = new HashSet<string>();
        private string TagItem { get; set; }
        private async void OnInputFileChanged(InputFileChangeEventArgs e)
        {
       
            var file = e.GetMultipleFiles().First();
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                Stream stream = file.OpenReadStream(file.Size+100);

                var fileStreamContent = new StreamContent(stream);
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue($"{file.ContentType}");
                content.Add(fileStreamContent,  "file", $"{file.Name}");

                HttpClient httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(Navigator.BaseUri),
                };
                httpClient.DefaultRequestHeaders.Add("API_KEY", "29122002Az@");
                var reponse = await httpClient.PostAsync("api/uploadfile", content);
                UrlSaveToDataBase = JsonConvert.DeserializeObject<dynamic>(await reponse.Content.ReadAsStringAsync()).url;
                UrlImagePost = @Navigator.BaseUri+ UrlSaveToDataBase;
                Snackbar.Add("Đã upload file", MudBlazor.Severity.Success);


            }
            StateHasChanged();
        }

        private async Task Clear()
        {
            Clearing = true;
            UrlImagePost = null;
            UrlSaveToDataBase = null;  
            ClearDragClass();
            await Task.Delay(100);
            Clearing = false;

        }
   
        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }
      
        public void AddItem()
        {
            if (Tags.Contains(TagItem))
            {
                SaveChanges("Tag đã được thêm, vui lòng thêm tag khác", MudBlazor.Severity.Info);
                return;
            }
            Tags.Add(TagItem);
            StateHasChanged();
        }
        public void RemoveTagItem(string tagChip)
        {
            Tags.Remove(tagChip);
            StateHasChanged();
        }
        public async Task GetDetailPost()
        {
           
                GetPostByIdQuery query = new GetPostByIdQuery()
                {
                    IdPost = new Guid(Id),
                };
                var result= await QueryBus.Send(query);
                PostDetail = result.Data;
            Basic_CheckBox1 = PostDetail.Published != null;
            if (PostDetail == null)
            {
                Navigator.NavigateTo("pages/error/404");
            }
               
            
        }
        public async Task CreateOrUpdatePost()
        {
            try
            {
                CreateOrUpdatePostCommand PostItem = new CreateOrUpdatePostCommand()
                {
                    Id = PostDetail.Id,
                  
                    ShortContent = PostDetail.ShortContent,
                    Header = PostDetail.Header,
                    UrlImage = UrlSaveToDataBase,
                    IsVisible = Basic_CheckBox1,
                    HeaderLink = PostDetail.Header.Replace(" ", "-"),
                    Tags = Tags.ToList(),
                    //Content= PostDetail.Content,
                };
                PostItem.Content = await GetContentStringHtmlEditor();
                await CommandBus.Send<Result<Unit>>(PostItem, default);
                SaveChanges("Profile details saved", MudBlazor.Severity.Success);
                if (Id == null)
                {
                    Navigator.NavigateTo($"application/post/{PostDetail.Id.ToString()}");

                }
            }
            catch(ValidationException ex)
            {
                var erors = ex.Errors;
                foreach(var er in erors)
                {
                    SaveChanges(er.ErrorMessage, MudBlazor.Severity.Error);

                }
            }

        }
    
        bool open;



        protected override async Task OnInitializedAsync()
        {
            editorOptions = new EditorOptions();
            var txt = await Task.Run(() => 1);
            var GuId = Guid.NewGuid();
            states = (await QueryBus.Send<Result<List<TagForResponse>>>(new GetAllTagQuery(), default)).Data.Select(p => p.Name).ToArray();
            if (Id == null)
            {
                Id = GuId.ToString();
                PostDetail = new PostDetail()
                {
                    Id = GuId,
                    Header = "",
                    Content = "",
                    Created = DateTimeOffset.Now,
                    ImagePost = "",
                    ShortContent = "",

                };
            }
            else
            {

                await GetDetailPost();
                UrlSaveToDataBase = PostDetail.ImagePost;
                UrlImagePost= @Navigator.BaseUri + PostDetail.ImagePost;
                if (PostDetail.Tags.Count() > 0)
                {
                    foreach(var item in PostDetail.Tags)
                    {
                        Tags.Add(item.Name);
                    }
                }
            }
            StateHasChanged();

        }

      
        void SaveChanges(string message, MudBlazor.Severity severity)
        {
            Snackbar.Add(message, severity, config =>
            {
                config.ShowCloseIcon = false;
            });
        }
        void OpenDrawer()
        {
            open = true;
        }

        private async ValueTask<string> GetContentStringHtmlEditor()
        {
            return await _jsRuntime.InvokeAsync<string>("getData", editor.EditorId);
        }
    }
}
