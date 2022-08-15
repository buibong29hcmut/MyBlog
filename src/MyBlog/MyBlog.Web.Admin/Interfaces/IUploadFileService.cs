namespace MyBlog.Web.Admin.Interfaces
{
    public interface IUploadFileService
    {
        public Task<string> UploadFileAsync(MultipartFormDataContent content);
    }
}
