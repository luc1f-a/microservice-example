using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using TBot.Common.Exceptions;

namespace TBot.WebApp
{
    public class AppMiddlewareException
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public AppMiddlewareException(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (AggregateException exp)
            {
                await HandleExceptionAsync(context, exp.GetBaseException(), HttpStatusCode.InternalServerError);
            }
            catch (Exception exp)
            {
                await HandleExceptionAsync(context, exp, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exp, HttpStatusCode code)
        {
            var result = JsonConvert.SerializeObject(exp);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            _logger.Error("{Message}\n{Trace}", exp.Message, exp.StackTrace);
            return context.Response.WriteAsync(result);
        }
    }
}