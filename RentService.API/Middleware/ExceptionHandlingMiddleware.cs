using RentService.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace RentService.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            HttpStatusCode code;
            object result;

            switch (exception)
            {
                case ValidationException validationEx:
                    code = HttpStatusCode.UnprocessableEntity;
                    result = new
                    {
                        Title = "Ошибки валидации",
                        Details = validationEx.Message,
                        Errors = validationEx.Errors
                    };
                    break;

                case BadRequestException badRequest:
                    code = HttpStatusCode.BadRequest;
                    result = new { Title = "Неверный запрос", Details = badRequest.Message };
                    break;

                case NotFoundException notFound:
                    code = HttpStatusCode.NotFound;
                    result = new { Title = "Не найдено", Details = notFound.Message };
                    break;

                case UnauthorizedException unauthorized:
                    code = HttpStatusCode.Unauthorized;
                    result = new { Title = "Ошибка авторизации", Details = unauthorized.Message };
                    break;

                case ForbiddenException forbidden:
                    code = HttpStatusCode.Forbidden;
                    result = new { Title = "Доступ запрещён", Details = forbidden.Message };
                    break;

                case ConflictException conflict:
                    code = HttpStatusCode.Conflict;
                    result = new { Title = "Конфликт", Details = conflict.Message };
                    break;

                case ApiException apiEx:
                    code = apiEx.StatusCode;
                    result = new { Title = "Ошибка API", Details = apiEx.Message };
                    break;

                default:
                    logger.LogError(exception, "Необработанная ошибка");
                    code = HttpStatusCode.InternalServerError;
                    result = new
                    {
                        Title = "Неизвестная ошибка",
                        Details = $"Произошла непредвиденная ошибка, подробнее: {exception.Message}"
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
}

