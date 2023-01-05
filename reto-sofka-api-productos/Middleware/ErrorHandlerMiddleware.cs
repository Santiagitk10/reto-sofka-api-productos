using reto_sofka_api_productos.ErrorModels;
using reto_sofka_api_productos.Exceptions;
using System.Net;
using Newtonsoft.Json;
using FluentValidation.Results;


namespace reto_sofka_api_productos.Middleware
{
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case ElementNotFoundException:
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        var errorModelNotFound = new ErrorBuilder()
                        .WithErrorCode("01")
                        .WithErrorMessage(ex.Message)
                        .Build();
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModelNotFound));
                        break;

                    case InvalidElementException<List<ValidationFailure>> invalidElementContext:

                        Dictionary<string, string> errors = new Dictionary<string, string>();

                        foreach (var error in invalidElementContext.Data)
                        {
                            var casterror = error;
                            errors.Add(casterror.PropertyName, casterror.ErrorMessage);
                        }

                        var errorModelBadRequest = new ErrorBuilder()
                        .WithErrorCode("02")
                        .WithErrorMessage(ex.Message)
                        .WithInformation(errors)
                        .Build();

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModelBadRequest));

                        break;

                    case InconsistentDataException:
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var errorModelInconsistentData = new ErrorBuilder().WithErrorCode("03").WithErrorMessage(ex.Message).Build();
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModelInconsistentData));
                        break;

                }
            }
        }

    }
}
