using System.Threading.Tasks;
using api.Middleware.Interface;
using Microsoft.AspNetCore.Http;

namespace api.Middleware
{
    public class SecureHeaderMiddleware
    {
        private readonly RequestDelegate Next;
        private IHeaderPolicy Policy { get; }
        public SecureHeaderMiddleware(RequestDelegate next, IHeaderPolicy policy)
        {
            Next = next;
            Policy = policy;
        }

        public async Task Invoke(HttpContext context)
        {
            Policy.AddPolicyToHeader(context.Response.Headers);
            await Next(context);
        }
    }
}