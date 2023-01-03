using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace reto_sofka_api_productos.ErrorModels
{
    public class ErrorModel
    {

        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Information { get; set; }

    }
}
