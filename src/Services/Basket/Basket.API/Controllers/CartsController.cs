using Basket.Domain.AggregateModels.BasketAggregate;
using Basket.Infrastructure.Persistence;
using EventBus.IntegrationEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly BasketDbContext _context;
        private readonly IBasketRepository _basketRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CartsController(BasketDbContext context, IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _basketRepository = basketRepository;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            return await _context.Carts.Include(c => c.BasketItems).ToListAsync();
        }


        // GET: api/Carts
        [HttpPost("AddItemToCart")]
        public async Task<ActionResult<IEnumerable<Cart>>> AddItemToCart(long cartId, CartItem cartItem)
        {
            var result = await _basketRepository.AddItemToBasket(cartId, cartItem);
            return Ok(result);
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(string username, int cartId)
        {
            var basketCheckout = await _context.Carts.Include(c => c.BasketItems).Where(c => c.UserName == username).FirstOrDefaultAsync();
            if (basketCheckout != null)
            {
                await _publishEndpoint.Publish(new BasketCheckout { Id = basketCheckout.Id, DocumentId = "1324", TotalPrice = 5000, UserName = basketCheckout.UserName });

                await _publishEndpoint.Publish(new BasketCheckout { Id = basketCheckout.Id, DocumentId = "1324", TotalPrice = 5000, UserName = basketCheckout.UserName });

                return Ok();
            }
            return NotFound();
        }


        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(long id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(long id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'BasketDbContext.Carts'  is null.");
            }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(long id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(long id)
        {
            return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
