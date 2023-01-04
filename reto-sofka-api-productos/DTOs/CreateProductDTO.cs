namespace reto_sofka_api_productos.DTOs
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; }
        public int InInventory { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
