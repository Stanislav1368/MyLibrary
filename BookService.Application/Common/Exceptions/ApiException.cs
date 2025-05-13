using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.Common.Exceptions
{
    public abstract class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected ApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
