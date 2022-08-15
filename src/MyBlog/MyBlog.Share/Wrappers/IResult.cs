using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Share.Wrappers
{
    public interface IResult
    {
        public List<string> Messages { get; set; }
        public bool Succeeded { get; set; }
    }
    public interface IResult<out TResponse>
    {
        TResponse Data { get; }
    }
}
