using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Application.Common.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message = "Требуется авторизация")
            : base(message, HttpStatusCode.Unauthorized) { }
    }
}
