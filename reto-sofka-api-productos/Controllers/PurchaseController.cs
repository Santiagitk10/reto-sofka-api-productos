using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reto_sofka_api_productos.DTOs;
using reto_sofka_api_productos.Helpers;
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


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllPurchases()
        {
            List<PurchaseJoinedDataDTO> purchases = await _service.GetAllPurchasesAsync();

            if (purchases.Count() == 0)
            {
                return NoContent();
            }

            return Ok(purchases);
        }


        [HttpPost("Post")]
        public async Task<IActionResult> CreatePurchase(CreatePurchaseDTO createPurchaseDTO)
        {
            return Ok(await _service.CreatePurchaseAsync(createPurchaseDTO));
        }

    }
}
