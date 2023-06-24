using Basket.Domain.AggregateModels.BasketAggregate;
using Basket.Infrastructure.Persistence;
using Infrastructure.Common.Repositories;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : RepositoryBase<Cart, long, BasketDbContext>, IBasketRepository
    {
        public BasketRepository(BasketDbContext context) : base(context)
        {
        }

        public async Task<Cart> AddItemToBasket(long basketId, CartItem item)
        {
            var cart = await FindByIdAsync(basketId);
            if (cart != null)
            {
                cart.AddItem(item);
                await Save();
                return cart;
            }

            return null;

        }

        public async Task<Cart> UpdateBasket(Cart cart)
        {
            var updateCart = await FindByIdAsync(cart.Id);
            if (updateCart != null)
            {

            }
            throw new NotImplementedException();
        }

        public async Task CreateBasket(Cart cart)
        {
            await AddAsync(cart);
            await Save();
        }

        public async Task<List<Cart>> GetAllBasket()
        {
            return GetAll().ToList();
        }

        public async Task<Cart> GetBasketById(int id)
        {
            return await FindByIdAsync(id);
        }



    }
}
