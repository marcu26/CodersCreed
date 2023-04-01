using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (InvalidJwtException ex)
            {
                await RespondToException(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (WrongInputException ex)
            {
                await RespondToException(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await RespondToException(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex);
            }
        }

        private static Task RespondToException(HttpContext context, HttpStatusCode failureStatusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)failureStatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                message = message,
                timestamp = DateTime.UtcNow
            }));
        }

        private static Task RespondToException(HttpContext context, HttpStatusCode failureStatusCode, string message, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)failureStatusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                message = message,
                details = exception.Message,
                type = exception.GetType().Name,
                timestamp = DateTime.UtcNow
            }));
        }
    }
}
