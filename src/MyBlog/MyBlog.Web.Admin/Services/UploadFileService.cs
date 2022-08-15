using MyBlog.Application.Interfaces.Services.Admin;
using MyBlog.Web.Admin.Interfaces;

namespace MyBlog.Web.Admin.Services
{
    public class UploadFileService:IUploadFileService
    {
        private readonly HttpClient _httpClient;
        public UploadFileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }   
        public async Task<string> UploadFileAsync(MultipartFormDataContent content)
        {
            _httpClient.DefaultRequestHeaders.Add("API_KEY", "29122002Az@");
            var postResult = await _httpClient.PostAsync("https://localhost:5011/api/upload", content);
            var urlResponse = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(urlResponse);
            }
           return urlResponse;

        }
    }
}
