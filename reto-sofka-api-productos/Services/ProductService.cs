using AutoMapper;
using DB;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Exceptions;
using reto_sofka_api_productos.Helpers;

namespace reto_sofka_api_productos.Services
{
    public class ProductService : IProductService
    {


        private readonly StoreContext _context;
        private readonly IValidator<CreateProductDTO> _validator;

        private readonly IMapper _mapper;

        public ProductService(
            StoreContext context,
            IValidator<CreateProductDTO> validator,
            IMapper mapper
        )
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }


        public async Task<List<GetProductDTO>> GetAllProductsAsync(ProductParameters productParameters)
        {
            List<Product> productsEntity = await _context.Products.Where(p => p.isEnabled != false)
                .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
                .Take(productParameters.PageSize)
                .ToListAsync();

            List<GetProductDTO> productsDTO = _mapper.Map<List<Product>, List<GetProductDTO>>(productsEntity);

            return productsDTO;
        }


        public async Task<GetProductDTO>? GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product is null)
            {
                throw new ElementNotFoundException($"Product with ID: {id} could not be found");
            }

            if (!product.isEnabled)
            {
                throw new InconsistentDataException($"Product with ID: {id} was deleted (Is not Enabled)");
            }

            var productDTO = _mapper.Map<Product, GetProductDTO>(product);
            return productDTO;
        }





        public async Task<CreateProductDTO> CreateProductAsync(CreateProductDTO productDTO)
        {
            
            var validationResult = await _validator.ValidateAsync(productDTO);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors;
                throw new InvalidElementException<List<ValidationFailure>>("Invalid arguments", errors);
            }

            Product productEntity = _mapper.Map<Product>(productDTO);
            productEntity.isEnabled = true;
            await _context.AddAsync(productEntity);
            await _context.SaveChangesAsync();
            return productDTO;

        }


        public async Task UpdateProductAsync(int id, CreateProductDTO productDTO)
        {

            var productEntity = await _context.Products.FindAsync(id);

            if (productEntity is null)
            {
                throw new ElementNotFoundException($"Product with ID: {id} could not be found");
            }

            var validationResult = await _validator.ValidateAsync(productDTO);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors;
                throw new InvalidElementException<List<ValidationFailure>>("Invalid arguments", errors);
            }

            productEntity.ProductName = productDTO.ProductName;
            productEntity.InInventory = productDTO.InInventory;
            productEntity.isEnabled = true;
            productEntity.Min = productDTO.Min;
            productEntity.Max = productDTO.Max;
            await _context.SaveChangesAsync();


        }




        public async Task DeleteProductByIdAsync(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);

            if (productEntity is null)
            {
                throw new ElementNotFoundException($"Product with ID: {id} could not be found");
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

        }



        
    }
}
