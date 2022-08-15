using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces.Services.Admin
{
    public interface ISaveFileService
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
