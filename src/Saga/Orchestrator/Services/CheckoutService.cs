using Orchestrator.HttpRepositories.Interfaces;
using Orchestrator.HttpRepository.Interfaces;
using Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;

namespace Orchestrator.Services;

public class CheckoutSagaService : ICheckoutSagaService
{
    private readonly IOrderHttpRepository _orderHttpRepository;
    private readonly IBasketHttpRepository _basketHttpRepository;
    private readonly IInventoryHttpRepository _inventoryHttpRepository;

    public CheckoutSagaService(IOrderHttpRepository orderHttpRepository, IBasketHttpRepository basketHttpRepository, IInventoryHttpRepository inventoryHttpRepository)
    {
        _orderHttpRepository = orderHttpRepository;
        _basketHttpRepository = basketHttpRepository;
        _inventoryHttpRepository = inventoryHttpRepository;
    }

    public async Task<bool> CheckoutOrder(long cartId)
    {
        try
        {
            var basket = await _basketHttpRepository.GetBasketAsync(cartId);

            //_orderHttpRepository.CreateOrder(basket);

            List<CartItemDto> CartItemDto = basket.BasketItems.ToList();

            foreach (var item in CartItemDto)
            {
                //_inventoryHttpRepository.UpdateStock(item);
            }

            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            await Rollback();
            return await Task.FromResult(false);

        }


    }

    private async Task Rollback()
    {
        await Console.Out.WriteLineAsync("Roll Back Checkout Order");
    }
}