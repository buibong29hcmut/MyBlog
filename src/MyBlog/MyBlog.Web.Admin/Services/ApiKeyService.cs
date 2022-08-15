using MyBlog.Web.Admin.Interfaces;

namespace MyBlog.Web.Admin.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IConfiguration configuration;
        private static string apiSection = "APIKEY";
        public ApiKeyService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async  Task<bool> ValidateApiKeyAsync(string ApiKeyFromRequest)
        {
            string APIKEY = configuration.GetSection(apiSection).Value;
            if (APIKEY == ApiKeyFromRequest)
                return await Task.FromResult(true);
            return await Task.FromResult(true);
        }
    }
}
