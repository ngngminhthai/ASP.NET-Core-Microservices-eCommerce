using Orchestrator.HttpRepositories.Interfaces;
using Shared.DTOs.Basket;

namespace Orchestrator.HttpRepository;

public class BasketHttpRepository : IBasketHttpRepository
{
    private readonly HttpClient _client;

    public BasketHttpRepository(HttpClient client)
    {
        _client = client;
    }

    public Task<bool> DeleteBasket(string username)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> GetBasket(string username)
    {
        throw new NotImplementedException();
    }
}