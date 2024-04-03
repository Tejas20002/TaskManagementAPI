using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JellBreak
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the user is authenticated
            if (context.User.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }

}
