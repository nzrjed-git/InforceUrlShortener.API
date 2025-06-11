using InforceUrlShortener.Domain.Exceptions;

namespace InforceUrlShortener.API.Middlewares
{
    public class ErrorHandlingMiddleware(
        ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
                logger.LogError(ex.Message);
            }
            catch (NotFoundByShortCodeException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
                logger.LogError(ex.Message);
            }
            catch (OriginalUrlDuplicateException ex)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);
                logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
