using MyBlog.Web.App.SEOs;

namespace MyBlog.Web.App
{
    public class MetaDataState
    {
        SeoModel model=new SeoModel();
        public SeoModel Model
        {
            get => model;
            set
            {
                if (model==(value))
                {
           
                    return;
                }
                model = value;
                CurrentMetaDataChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<SeoModel> CurrentMetaDataChanged;
    }
}
