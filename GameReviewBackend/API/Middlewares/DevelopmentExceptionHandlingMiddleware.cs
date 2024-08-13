using System.Net;
using System.Text.Json;
using Components.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models.ODataErrors;

namespace API.Middlewares
{
    public class DevelopmentExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<DevelopmentExceptionHandlingMiddleware> _logger;

        public DevelopmentExceptionHandlingMiddleware(ILogger<DevelopmentExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(DgcException e)
            {
                _logger.LogError(e, e.Message);

                int status = 400;

                switch(e.Type)
                {
                    case DgcExceptionType.ResourceNotFound:
                        status = (int)HttpStatusCode.NotFound;
                        break;
                    case DgcExceptionType.Unauthorized:
                        status = (int)HttpStatusCode.Unauthorized;
                        break;
                    case DgcExceptionType.Forbidden:
                        status = (int)HttpStatusCode.Forbidden;
                        break;
                    case DgcExceptionType.InvalidArgument:
                    case DgcExceptionType.ArgumentNull:
                    case DgcExceptionType.ArgumentOutOfRange:
                    case DgcExceptionType.InvalidOperation:
                    case DgcExceptionType.NotSupported:
                    default:
                        status = (int)HttpStatusCode.BadRequest;
                        break;
                }

                context.Response.StatusCode = status;
                ProblemDetails problemDetails = new()
                {
                    Status = status,
                    Type = "Bad Request",
                    Title = "Bad Request",
                    Detail = e.Message
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
            catch (ODataError e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                ProblemDetails problemDetails = new()
                {
                    Status = (int)HttpStatusCode.Unauthorized,
                    Type = "Unauthorized Request",
                    Title = "Unauthorized Request",
                    Detail = e.Message + "\n" + e.StackTrace + "\n\n" + e.InnerException?.Message + "\n" + e.InnerException?.StackTrace
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);                
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ProblemDetails problemDetails = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = e.Message + "\n" + e.StackTrace + "\n\n" + e.InnerException?.Message + "\n" + e.InnerException?.StackTrace,
                };

                string json = JsonSerializer.Serialize(problemDetails);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }
    }
}