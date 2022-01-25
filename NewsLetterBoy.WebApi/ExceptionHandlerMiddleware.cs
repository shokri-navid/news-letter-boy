using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model;
using NewsLetterBoy.WebApi.Dto.Response;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NewsLetterBoy.WebApi
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var message = "";
                switch(error)
                {
                    case DomainException e:
                        message = e.Message;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ApplicationException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        message = e.Message;
                        break;
                    default :
                        // unhandled error
                        _logger.LogError(error, error.Message);
                        message = "Server Error.";
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var responseBody = new ResponseBase(new[] {message}); 
                var result = JsonSerializer.Serialize(responseBody);
                await response.WriteAsync(result);
            }

        }
    }
}