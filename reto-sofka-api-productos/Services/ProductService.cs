using AutoMapper;
using DB;
using FluentValidation;
using FluentValidation.Results;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Exceptions;

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


        public Task<List<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        Task<Product>? IProductService.GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
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









        Task IProductService.DeleteProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



        Task IProductService.UpdateProductAsync(CreateProductDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
