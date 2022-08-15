using Microsoft.Extensions.Primitives;

namespace MyBlog.Web.Admin
{
    public class ApiKeyMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration configuration;
        private readonly string _apiKeyConfig = "APIKEY";

        public ApiKeyMiddleWare(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            this.configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var apikey = configuration.GetSection(_apiKeyConfig).Value;

            //we could inject here our database context to do checks against the db
            if (httpContext.Request.Headers.TryGetValue("API-KEY", out StringValues value))
            {

                if (apikey == value)
                {
                    await _next(httpContext);
                }
            }
            else
            {
                //return 403
                httpContext.Response.StatusCode = 403;
            }

        }
    }
}
