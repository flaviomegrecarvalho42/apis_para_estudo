
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParaEstudoApi.DTO.Error;
using ParaEstudoApi.Util.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ParaEstudoApi.Util.Middlewares
{
    /// <summary>
    /// Middleware to capture global exceptions.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IStringLocalizer<ErrorHandlingMiddleware> _localizer;

        /// <summary>
        /// Middleware constructor.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public ErrorHandlingMiddleware(RequestDelegate next,
                                       ILogger<ErrorHandlingMiddleware> logger,
                                       IStringLocalizer<ErrorHandlingMiddleware> localizer)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        /// <summary>
        /// Middleware Invoke.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ParaEstudoApiException paraEstudoApiException)
            {
                _logger.LogWarning("{0} | {1}", context.TraceIdentifier, paraEstudoApiException.Message);
                await Error(context, _localizer[paraEstudoApiException.Message], paraEstudoApiException.StatusCode);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{0} | {1}", context.TraceIdentifier, exception.Message);
                await Error(context, "Internal Error Server", 500);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private async Task Error(HttpContext context, string message, int statusCode)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = @"application/json";

                await context.Response
                             .WriteAsync(JsonConvert.SerializeObject(new ErrorDto { StatusCode = statusCode, 
                                                                                    Message = message, 
                                                                                    Title = Enum.Parse(typeof(HttpStatusCode), 
                                                                                                       statusCode.ToString()).ToString() }));
            }
        }
    }
}
