using Shared.DTOs.Basket;

namespace Orchestrator.HttpRepositories.Interfaces
{
    public interface IBasketHttpRepository
    {
        Task<CartDto> GetBasket(string username);
        Task<bool> DeleteBasket(string username);
    }
}
