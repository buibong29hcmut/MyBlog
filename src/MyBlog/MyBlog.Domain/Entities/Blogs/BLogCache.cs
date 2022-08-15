using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Blogs
{
    public class BLogCache:IEntity
    {
        public string Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset? ExpiresAtTime { get; set; }
        public int SlidingExpirationInSeconds { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
    }
}
