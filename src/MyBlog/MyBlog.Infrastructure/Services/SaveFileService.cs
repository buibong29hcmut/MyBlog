using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyBlog.Application.Interfaces.Services.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyBlog.Infrastructure.Services
{
    public class SaveFileService: ISaveFileService
    {
        private readonly IHostingEnvironment hosting;
        public SaveFileService(IHostingEnvironment hosting)
        {
            this.hosting = hosting;
            
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string wwwPath = hosting.WebRootPath+"/Images/";
            string fileName = Guid.NewGuid().ToString().Substring(0, 8);

            using (var stream = System.IO.File.Create(wwwPath+$"{fileName}{fileExtension}"))
            {
                await file.CopyToAsync(stream);
            }


            return   "/Images/" + $"{fileName}{fileExtension}";
        }
    }
}
