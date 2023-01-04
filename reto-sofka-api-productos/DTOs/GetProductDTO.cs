namespace reto_sofka_api_productos.DTOs
{
    public class GetProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int InInventory { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

    }
}
