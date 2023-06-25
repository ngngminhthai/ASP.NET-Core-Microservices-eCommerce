using Microsoft.AspNetCore.Mvc;
using Orchestrator.Services.Interfaces;

namespace Orchestrator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutSagaService _checkoutSagaService;

        public CheckoutController(ICheckoutSagaService checkoutSagaService)
        {
            _checkoutSagaService = checkoutSagaService;
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutOrder(long cartId)
        {
            var result = await _checkoutSagaService.CheckoutOrder(cartId);
            return Accepted(result);
        }
    }
}
