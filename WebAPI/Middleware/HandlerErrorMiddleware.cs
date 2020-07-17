using System;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class HandlerErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<HandlerErrorMiddleware> _logger;

        public HandlerErrorMiddleware(RequestDelegate next, ILogger<HandlerErrorMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext context, Exception ex, ILogger<HandlerErrorMiddleware> logger)
        {
            object errors = null;
            switch(ex)
            {
                case HandlerException me:
                    logger.LogError(ex, "Handler Error");
                    errors = me.Errors;
                    context.Response.StatusCode = (int)me.Code;
                    break;
                
                case Exception e:
                    logger.LogError(ex, "Server Error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break; 
            }

            context.Response.ContentType = "application/json";
            if(errors != null)
            {
                var results = JsonConvert.SerializeObject(new {errors});
                await context.Response.WriteAsync(results);
            }
        }
    }
}