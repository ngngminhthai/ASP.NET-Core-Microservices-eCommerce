using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Application.Features.ProductItems.Commands.UpdateProductItem;
using Product.Domain.AggregateModels.ProductAggregate;
using Product.Infrastructure.Persistence;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly ProductDbContext _context;
        private readonly IMediator _mediator;


        public ProductItemsController(ProductDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }

        // GET: api/ProductItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        // GET: api/ProductItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(long id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var productItem = await _context.Products.FindAsync(id);

            if (productItem == null)
            {
                return NotFound();
            }

            return productItem;
        }

        // PUT: api/ProductItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductItem(long id, ProductItem productItem)
        {
            if (id != productItem.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(new UpdateProductItemCommand(productItem));

            //Create of do something else logic

            if (result.IsSucceeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        // POST: api/ProductItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductItem>> PostProductItem(ProductItem productItem)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ProductDbContext.Products'  is null.");
            }
            _context.Products.Add(productItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductItem", new { id = productItem.Id }, productItem);
        }

        // DELETE: api/ProductItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductItem(long id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var productItem = await _context.Products.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductItemExists(long id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
