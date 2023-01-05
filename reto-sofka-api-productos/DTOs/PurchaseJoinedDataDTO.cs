using DB;

namespace reto_sofka_api_productos.DTOs
{
    public class PurchaseJoinedDataDTO
    {
        
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }
        public string IdType { get; set; }
        public string Id { get; set; }
        public string ClientName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }


    }
}
