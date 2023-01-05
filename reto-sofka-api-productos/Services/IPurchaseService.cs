using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Helpers;

namespace reto_sofka_api_productos.Services
{
    public interface IPurchaseService
    {

        Task<List<PurchaseJoinedDataDTO>>  GetAllPurchasesAsync();
        Task<CreatePurchaseDTO> CreatePurchaseAsync(CreatePurchaseDTO purchase);

    }
}
