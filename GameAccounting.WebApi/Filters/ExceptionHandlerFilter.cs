using GameAccounting.BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GameAccounting.WebApi.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int code = 500;
            if (context.Exception is ApiResponseException)
            {
                var resEx = (ApiResponseException)context.Exception;
                code = (int)resEx.Code;
            }
            context.Result =
                new ContentResult()
                {
                    Content = context.Exception.Message,
                    StatusCode = code
                };

            context.ExceptionHandled = true;
        }
    }
   
}
