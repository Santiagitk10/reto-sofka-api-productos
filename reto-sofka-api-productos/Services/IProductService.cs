using DB;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Helpers;

namespace reto_sofka_api_productos.Services
{
    public interface IProductService
    {
        Task<List<GetProductDTO>> GetAllProductsAsync(ProductParameters productParameters);
        Task<GetProductDTO>? GetProductByIdAsync(int id);
        Task<CreateProductDTO> CreateProductAsync(CreateProductDTO product);
        //Task UpdateProductAsync(int id, CreateProductDTO product);
        Task DeleteProductByIdAsync(int id);

    }
}
