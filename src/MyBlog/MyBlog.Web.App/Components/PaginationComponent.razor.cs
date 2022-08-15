using Microsoft.AspNetCore.Components;
using static MudBlazor.Colors;

namespace MyBlog.Web.App.Components
{
    public partial class PaginationComponent
    {
        [Parameter]
        public int CountPage { get; set; } 
        [Parameter]
        public int CurrentPage { get; set; }
        [Parameter]
        public string UrlFormat { get; set; }
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }
        protected  async Task ChangeValue()
        {
            await ValueChanged.InvokeAsync(CurrentPage);

        }
    }
}
