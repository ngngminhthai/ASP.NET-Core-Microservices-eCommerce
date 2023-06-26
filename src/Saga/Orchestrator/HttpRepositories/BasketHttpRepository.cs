using Orchestrator.HttpRepositories.Interfaces;
using Shared.DTOs.Basket;

namespace Orchestrator.HttpRepository;

public class BasketHttpRepository : IBasketHttpRepository
{
    private readonly HttpClient _httpClient;

    public BasketHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<bool> DeleteBasketAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDto> GetBasketAsync(long cartId)
    {
        var cart = await _httpClient.GetFromJsonAsync<CartDto>($"Carts/GetBasket/{cartId}");
        if (cart == null || !cart.BasketItems.Any()) return null;

        return cart;
    }
}