using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ServerTimeHeaderMiddleware
{
    private readonly RequestDelegate _next;
    public ServerTimeHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() => {
            context.Response.Headers["X-Server-Time"] = System.DateTime.Now.ToString("O");
            return Task.CompletedTask;
        });
        await _next(context);
    }
}
