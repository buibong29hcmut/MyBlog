using MyBlog.Domain.Common;
using System.IO;

namespace MyBlog.Web.App.SEOs
{
    public class SeoModel
    {
        public string Title { get; set; } = "";
        public List<MetaDataModel> MetaData { get; set; }
   
    }
}
