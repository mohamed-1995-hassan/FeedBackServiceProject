using FeedBackServiceProject.Core.Exceptions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;
using System.Text;

namespace FeedBackServiceProject.Api.Middlewares
{

    public static class HttpStatusCodeExceptionMiddlewareExtentions
    {
        public static IApplicationBuilder UseHttpCodeAndLogMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpCodeAndLogMiddleware>();
        }
    }
    public class HttpCodeAndLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpCodeAndLogMiddleware> _logger;

        public HttpCodeAndLogMiddleware(RequestDelegate next,ILogger<HttpCodeAndLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                return;
            }
            try
            {
                context.Request.EnableBuffering();
                await _next(context);
            }catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                switch(ex)
                {
                    case ApiException e:
                        context.Response.StatusCode =(int) HttpStatusCode.BadRequest;
                        await WriteAndLogResponseAsync(ex, context, HttpStatusCode.BadRequest, LogLevel.Error,"Bad Request Exception "+e.Message);
                        break;

                    case NotFoundException e:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await WriteAndLogResponseAsync(ex, context, HttpStatusCode.NotFound, LogLevel.Error, "Not Found Exception " + e.Message);
                        break;

                    case ValidationException e:
                        context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        await WriteAndLogResponseAsync(ex, context, HttpStatusCode.UnprocessableEntity, LogLevel.Error, "Validation Exception " + e.Message);
                        break;

                    case AuthenticationException e:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await WriteAndLogResponseAsync(ex, context, HttpStatusCode.Unauthorized, LogLevel.Error, "Unauthorized Exception " + e.Message);
                        break;


                    default:
                        await WriteAndLogResponseAsync(ex, context, HttpStatusCode.InternalServerError, LogLevel.Error, "Server Exception ");
                        break;

                }
            }
        }

        public async Task WriteAndLogResponseAsync(Exception exception, HttpContext httpContext, HttpStatusCode statusCode, LogLevel logLevel, string alternateMessage = null)
        {
            string requestBody=string.Empty;
            if(httpContext.Request.Body.CanSeek)
            {
                httpContext.Request.Body.Seek(0, System.IO.SeekOrigin.Begin);
                using (var st=new System.IO.StreamReader(httpContext.Request.Body))
                {
                    requestBody=JsonConvert.SerializeObject(st.ReadToEndAsync());
                }
            }
            StringValues authorization;
            httpContext.Request.Headers.TryGetValue("Authorization", out authorization);

            var customDetails = new StringBuilder();
            customDetails
                .AppendFormat("\n  Service URL    :").Append(httpContext.Request.Path.ToString())
                .AppendFormat("\n  Request Method :").Append(httpContext.Request?.Method)
                .AppendFormat("\n  Request Body   :").Append(requestBody)
                .AppendFormat("\n  Authorization  :").Append(authorization)
                .AppendFormat("\n  Content-Type   :").Append(httpContext.Request?.Headers["Content-Type"].ToString())
                .AppendFormat("\n  Request Cookie :").Append(httpContext.Request?.Headers["Cookie"].ToString())
                .AppendFormat("\n  Host           :").Append(httpContext.Request?.Headers["Host"].ToString())
                .AppendFormat("\n  Referer        :").Append(httpContext.Request?.Headers["Referer"].ToString())
                .AppendFormat("\n  Origin         :").Append(httpContext.Request?.Headers["Origin"].ToString())
                .AppendFormat("\n  User-Agent     :").Append(httpContext.Request?.Headers["User-Agent"].ToString())
                .AppendFormat("\n  Request Method :").Append(httpContext.Request?.Method)
                .AppendFormat("\n  Error Message  :").Append(exception.Message);
            _logger.Log(logLevel,exception,customDetails.ToString());
            if(httpContext.Response.HasStarted)
            {
                _logger.LogError("The Response has already started,the http status code Middle ware will not be excuted");
                return;
            }
            string responseMessage = JsonConvert.SerializeObject(
                new
                {
                    Message = string.IsNullOrWhiteSpace(exception.Message) ? alternateMessage : exception.Message
                });
            httpContext.Response.Clear();
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode =(int) statusCode;
            await httpContext.Response.WriteAsync(responseMessage,Encoding.UTF8);


        }

    }
}
