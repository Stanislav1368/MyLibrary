using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.Common.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest) { }
    }
}
