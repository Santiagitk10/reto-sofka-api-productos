using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Services;

namespace reto_sofka_api_productos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }


        [HttpPost("Post")]
        public async Task<IActionResult> CreatePurchase(CreatePurchaseDTO createPurchaseDTO)
        {
            return Ok(await _service.CreatePurchaseAsync(createPurchaseDTO));
        }

    }
}
