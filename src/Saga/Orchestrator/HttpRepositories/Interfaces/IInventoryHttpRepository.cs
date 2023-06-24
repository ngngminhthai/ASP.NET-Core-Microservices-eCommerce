using Shared.DTOs.Inventory;

namespace Orchestrator.HttpRepository.Interfaces;

public interface IInventoryHttpRepository
{
    Task<string> CreateSalesOrder(PurchasedProduct model);
}