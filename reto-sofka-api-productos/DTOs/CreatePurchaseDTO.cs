
namespace reto_sofka_api_productos.DTOs
{
    public class CreatePurchaseDTO
    {

        public string IdType { get; set; }
        public string Id { get; set; }
        public string ClientName { get; set; }
        public ICollection<string> ProductIDs { get; set; }


    }
}
