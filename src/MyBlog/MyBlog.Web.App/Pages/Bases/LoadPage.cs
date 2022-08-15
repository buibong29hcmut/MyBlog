using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace MyBlog.Web.App.Pages.Bases
{
    public  abstract class LoadPage: ComponentBase
    {
        private Boolean _showFooter = false;
        [Inject]
        private AppState _appState { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _appState.AppStateChanged += AppStateChanged;

            _showFooter = _appState.LoadingIsFinished;
        }

        private async void AppStateChanged(object sender, EventArgs args)
        {
            _showFooter = _appState.LoadingIsFinished;
            await InvokeAsync(StateHasChanged);
        }
      
        public void Dispose()
        {
            _appState.AppStateChanged -= AppStateChanged;
          
        }
    }
}
