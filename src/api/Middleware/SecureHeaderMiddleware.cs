using System.Threading.Tasks;
using api.Middleware.Interface;
using Components;
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

        private void AddToHeader(string header, string value, IHeaderDictionary headerList)
            => headerList.Add(header, value);

        private void RemoveFromHeader(string header, IHeaderDictionary headerList)
            => headerList.Remove(header);

        public async Task Invoke(HttpContext context)
        {
            var headers = context.Response.Headers;
            if (!Validation.ValueIsGreateherThan(Policy.Headers.Count, 0))
                Policy.BuildPolicies();

            foreach (var policy in Policy.Headers)
            {
                if (!policy.Remove)
                    AddToHeader(policy.Header, policy.Value, headers);
                else
                    RemoveFromHeader(policy.Header, headers);
            }
            await Next(context);
        }
    }
}