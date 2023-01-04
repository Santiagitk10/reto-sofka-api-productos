using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace reto_sofka_api_productos.ErrorModels
{
    public class ErrorBuilder
    {

        private string? _errorCode;
        private string? _errorMessage;
        private Dictionary<string, string> _information = new Dictionary<string, string>()
        {
            {"Info", "No information to show" }
        };

        public ErrorBuilder WithErrorCode(string errorCode)
        {
            _errorCode = errorCode;
            return this;
        }


        public ErrorBuilder WithErrorMessage(string errorMessage)
        {
            _errorMessage = errorMessage;
            return this;
        }

        public ErrorBuilder WithInformation(Dictionary<string, string> information)
        {
            _information = information;
            return this;
        }


        public ErrorModel Build()
        {
            return new ErrorModel
            {
                ErrorCode = _errorCode,
                Message = _errorMessage,
                Information = _information
            };
        }

    }
}
