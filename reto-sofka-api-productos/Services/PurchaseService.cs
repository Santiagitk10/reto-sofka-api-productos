using AutoMapper;
using DB;
using reto_sofka_api_productos.DTOs;

namespace reto_sofka_api_productos.Services
{
    public class PurchaseService : IPurchaseService
    {


        private readonly StoreContext _context;
        private readonly IMapper _mapper;


        public PurchaseService(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreatePurchaseDTO> CreatePurchaseAsync(CreatePurchaseDTO createPurchaseDTO)
        {
            Purchase purchaseEntity = _mapper.Map<Purchase>(createPurchaseDTO);
            purchaseEntity.Date = DateTime.Now;
            await _context.AddAsync(purchaseEntity);
            await _context.SaveChangesAsync();
            var id = purchaseEntity.PurchaseId;

            

            foreach (var productID in createPurchaseDTO.ProductIDs)
            {
                var productPurchase = new ProductPurchase();
                productPurchase.ProductId = int.Parse(productID);
                productPurchase.PurchaseId = id;
                await _context.AddAsync(productPurchase);
                await _context.SaveChangesAsync();
            }

            return createPurchaseDTO;
        }
    }
}
