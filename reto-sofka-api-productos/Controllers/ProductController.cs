using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Services;

namespace reto_sofka_api_productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("Post")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            return Ok(await _service.CreateProductAsync(productDTO));
        }

    }
}
