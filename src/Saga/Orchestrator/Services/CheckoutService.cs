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

    public Task<bool> CheckoutOrder(string username, CartDto basketCheckout)
    {
        return Task.FromResult(true);
    }
}