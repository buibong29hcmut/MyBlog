namespace MyBlog.Web.Admin.Interfaces
{
    public interface IApiKeyService
    {
        Task<bool> ValidateApiKeyAsync(string ApiKeyFromRequest);

    }
}
