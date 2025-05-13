using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.Common.Exceptions
{
    public class ValidationException : ApiException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Ошибки валидации", HttpStatusCode.UnprocessableEntity)
        {
            Errors = errors;
        }
    }
}
