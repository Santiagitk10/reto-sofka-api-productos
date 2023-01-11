using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Helpers;
using reto_sofka_api_productos.Services;

namespace reto_sofka_api_productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementationPolicy")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

   
        [HttpGet("Get") ]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters productParameters)
        {
            List<GetProductDTO> products = await _service.GetAllProductsAsync(productParameters);

            if (products.Count() == 0)
            {
                return NoContent();
            }

            return Ok(products);
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _service.GetProductByIdAsync(id));
        }




        [HttpPost("Post")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            return Ok(await _service.CreateProductAsync(productDTO));
        }




        [HttpPut("Put/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, EditProductDTO productDTO)
        {
            await _service.UpdateProductAsync(id, productDTO);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service.DeleteProductByIdAsync(id);
            return Ok();
        }

    }
}
