using Microsoft.AspNetCore.Http;
using PokemonAPI.Exceptions;
using System.Threading.Tasks;

namespace PokemonAPI.Middleware
{
    public class ErrorHandling : IMiddleware 
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
        }
    }
}
