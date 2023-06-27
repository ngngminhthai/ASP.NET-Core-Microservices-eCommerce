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

    private CartDto _basket;
    private long _orderId;

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
            _basket = await _basketHttpRepository.GetBasketAsync(cartId);

            //_orderId = await _orderHttpRepository.CreateOrder(_basket);

            foreach (var item in _basket.BasketItems)
            {
                //await _inventoryHttpRepository.UpdateStock(item);
            }

            return true;
        }
        catch (Exception ex)
        {
            await Rollback();
            return false;
        }
    }

    private async Task Rollback()
    {
        try
        {
            if (_orderId != default)
            {
                //await _orderHttpRepository.DeleteOrder(_orderId);
                _orderId = default;
            }

            if (_basket != null)
            {
                foreach (var item in _basket.BasketItems)
                {
                    //await _inventoryHttpRepository.RollbackStockUpdate(item);
                }
                _basket = null;
            }

            await Console.Out.WriteLineAsync("Roll Back Checkout Order");
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"Error during Rollback: {ex.Message}");
        }
    }
}