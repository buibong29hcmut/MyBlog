using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Queries.Abstracts
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    { }
}
