using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Neosoft_LeaveManagement.Exceptions;
using System;

namespace Neosoft_LeaveManagement.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            string message;
            int statusCode;

            if (exceptionType == typeof(LeaveBalanceExceededException))
            {
                message = "You do not have enough leave balance.";
                statusCode = 400;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "You are not authorized to perform this action.";
                statusCode = 403; 

            }
            else if (exceptionType == typeof(ArgumentException))
            {
                message = context.Exception.Message;
                statusCode = 400; 
            }
            else
            {
                message = "An unexpected error occurred.";
                statusCode = 500; 
            }

            context.Result = new JsonResult(new { error = message })
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}
