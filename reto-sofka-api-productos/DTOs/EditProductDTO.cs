namespace reto_sofka_api_productos.DTOs
{
    public class EditProductDTO
    {
        public string ProductName { get; set; }
        public int InInventory { get; set; }
        public bool isEnabled { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

    }
}
