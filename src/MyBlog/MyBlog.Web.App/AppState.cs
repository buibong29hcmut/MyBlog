namespace MyBlog.Web.App
{
    public class AppState
    {
        private Boolean _loadingIsFinished = false;

        public Boolean LoadingIsFinished
        {
            get => _loadingIsFinished;
            set
            {
                _loadingIsFinished = value;
                AppStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler AppStateChanged;
    }
}
