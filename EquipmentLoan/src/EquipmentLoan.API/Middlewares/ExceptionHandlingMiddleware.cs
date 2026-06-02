using System.Net;
using System.Text.Json;
using EquipmentLoan.Domain.Exceptions;

namespace EquipmentLoan.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocorreu um erro interno inesperado no servidor.";

            if (exception is BusinessException)
            {
                statusCode = HttpStatusCode.BadRequest; 
                message = exception.Message;
            }
            else if (exception.GetType().Name == "NotFoundException" || exception.Message.Contains("não encontrado"))
            {
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new { message = message };
            var jsonResponse = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}