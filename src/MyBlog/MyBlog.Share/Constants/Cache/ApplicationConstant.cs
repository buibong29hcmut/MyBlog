using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Share.Constants.Cache
{
    public static class ApplicationConstant
    {
        public static string cachePagingKey(int pageNumber)=> string.Format("page_{0}", pageNumber);
        public static string cacheTag = "Tags";
    }
}
