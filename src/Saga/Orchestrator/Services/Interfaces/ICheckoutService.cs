using Shared.DTOs.Basket;

namespace Orchestrator.Services.Interfaces;

public interface ICheckoutSagaService
{
    Task<bool> CheckoutOrder(string username, CartDto basketCheckout);
}