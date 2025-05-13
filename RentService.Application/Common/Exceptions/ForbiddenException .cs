using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Application.Common.Exceptions
{
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message = "Доступ запрещён")
            : base(message, HttpStatusCode.Forbidden) { }
    }
}
