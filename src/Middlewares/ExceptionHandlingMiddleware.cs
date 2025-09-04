using System.Net;
using System.Text.Json;
using ClientManager.src.Exceptions;
using ClientManager.src.DTOs;

namespace ClientManager.src.Middlewares;
public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await WriteErrorResponseAsync(context, ex);
        }
    }
    private static async Task WriteErrorResponseAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        int statusCode;
        string message;

        switch (exception)
        {
            case BadRequestException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            case NotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case RequestValidationException:
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
                message = "Ocorreram erros de validacao: " + exception.Message;
                break;
            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = "Erro interno do servidor!";
                Console.WriteLine(exception.Message);
                break;
        }

        response.StatusCode = statusCode;
        var payload = new ApiResponseMessage
        {
            Message = message, 
        };

        var json = JsonSerializer.Serialize(payload);
        await response.WriteAsync(json);
    }
}
