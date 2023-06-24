using Contracts.Domains.Interfaces;

namespace Basket.Domain.AggregateModels.BasketAggregate
{
    public interface IBasketRepository : IRepositoryBase<Cart, long>
    {
        Task<Cart> AddItemToBasket(long basketId, CartItem item);

        Task<Cart> UpdateBasket(Cart cart);

        Task CreateBasket(Cart cart);

        Task<List<Cart>> GetAllBasket();

        Task<Cart> GetBasketById(int id);
    }
}
