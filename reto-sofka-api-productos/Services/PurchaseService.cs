using AutoMapper;
using DB;
using Microsoft.EntityFrameworkCore;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Exceptions;

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



        public async Task<List<PurchaseJoinedDataDTO>> GetAllPurchasesAsync()
        {
            var allJoinedData = await _context.ProductPurchases.OrderBy(x => x.PurchaseId).Include(x => x.Purchase)
                .Include(x => x.Product).ToListAsync();

            List<PurchaseJoinedDataDTO> purchaseJoinedDataDTOs = new();

            foreach (var allJoinedDataItem in allJoinedData)
            {
                PurchaseJoinedDataDTO purchaseJoinedDataDTO = new();

                purchaseJoinedDataDTO.PurchaseId = allJoinedDataItem.PurchaseId;
                purchaseJoinedDataDTO.Date = allJoinedDataItem.Purchase.Date;
                purchaseJoinedDataDTO.IdType = allJoinedDataItem.Purchase.IdType;
                purchaseJoinedDataDTO.Id = allJoinedDataItem.Purchase.Id;
                purchaseJoinedDataDTO.ClientName = allJoinedDataItem.Purchase.ClientName;
                purchaseJoinedDataDTO.ProductId = allJoinedDataItem.ProductId;
                purchaseJoinedDataDTO.ProductName = allJoinedDataItem.Product.ProductName;

                purchaseJoinedDataDTOs.Add(purchaseJoinedDataDTO);
            }

            return purchaseJoinedDataDTOs;

        }








        public async Task<CreatePurchaseDTO> CreatePurchaseAsync(CreatePurchaseDTO createPurchaseDTO)
        {
            
            foreach (var productID in createPurchaseDTO.ProductIDs)
            {
                char[] delimitator = { '.' };
                var prodSplit = productID.Split(delimitator);
                var prodId = int.Parse(prodSplit[0]);
                var prodQuantity = int.Parse(prodSplit[1]);

                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == prodId);

                if (!product.isEnabled)
                {
                    throw new InconsistentDataException($"Product: {product.ProductName} with ID: {product.ProductId} is not available");
                }

                if (prodQuantity < product.Min || prodQuantity > product.Max)
                {
                    throw new InconsistentDataException($"Product: {product.ProductName} with ID: {product.ProductId} must be purchased within permitted amounts. Min: {product.Min}. Max: {product.Max}");
                }

                if (product.InInventory - prodQuantity < 0)
                {
                    throw new InconsistentDataException($"Product: {product.ProductName} with ID: {product.ProductId} is not available in the requested quantity. Stock: {product.InInventory}");
                }

                product.InInventory -= prodQuantity;
                await _context.SaveChangesAsync();

                Purchase purchaseEntity = _mapper.Map<Purchase>(createPurchaseDTO);
                purchaseEntity.Date = DateTime.Now;
                await _context.AddAsync(purchaseEntity);
                await _context.SaveChangesAsync();
                var id = purchaseEntity.PurchaseId;

                var productPurchase = new ProductPurchase();
                productPurchase.ProductId = prodId;
                productPurchase.PurchaseId = id;
                await _context.AddAsync(productPurchase);
                await _context.SaveChangesAsync();
            }

            return createPurchaseDTO;
        }

        

        
    }
}
