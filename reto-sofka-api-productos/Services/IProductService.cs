using DB;
using reto_sofka_api_productos.DTOs;

namespace reto_sofka_api_productos.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product>? GetProductByIdAsync(int id);
        Task<CreateProductDTO> CreateProductAsync(CreateProductDTO product);
        Task UpdateProductAsync(CreateProductDTO product);
        Task DeleteProductByIdAsync(int id);

    }
}
