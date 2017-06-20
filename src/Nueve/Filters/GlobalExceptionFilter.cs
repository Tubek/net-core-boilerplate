using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nueve.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Nueve.ViewModels.Error;

namespace Nueve.Filters
{
    /// <summary>
    /// GlobalExceptionFilter
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        readonly List<Type> _doNotLog = new List<Type>();
        private readonly ILog _logger;
        /// <summary>
        /// GlobalExceptionFilter
        /// </summary>
        /// <param name="logger"></param>
        public GlobalExceptionFilter(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _doNotLog.Clear();
        }

        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (ex != null && !_doNotLog.Contains(ex.GetType()))
            {
                _logger.Error(context.Exception);
                _doNotLog.Add(ex.GetType());
            }

            var response = new ErrorResponse();

            if (ex is NotSupportedException || ex is InvalidOperationException)
            {
                context.Result = new BadRequestObjectResult(ex?.ToString());
            }
            else if (ex is HttpResponseException)
            {
                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)((HttpResponseException)ex).Response.StatusCode,
                    DeclaredType = typeof(ErrorResponse)
                }; 
            }
            else
            {
                context.Result = new ObjectResult(response)
                {
                    StatusCode = 500,
                    DeclaredType = typeof (ErrorResponse)
                };

            }
        }
    }
}
