using Shared.DTOs.Basket;

namespace Orchestrator.HttpRepositories.Interfaces
{
    public interface IBasketHttpRepository
    {
        Task<CartDto> GetBasketAsync(long cartId);
        Task<bool> DeleteBasketAsync(string username);
    }
}
