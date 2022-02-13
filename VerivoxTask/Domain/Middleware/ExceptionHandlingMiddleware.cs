using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using VerivoxTask.Domain.Model;

namespace VerivoxTask.Extensions
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            ////The code was added in a scenario where exception types will have their own unique codes
            ///999 indicates general error
            
            var errorMessageObject = new Error { Message = ex.Message, Code = "999" };
            var statusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case VerivoxException:
                    errorMessageObject.Code = "V001";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
