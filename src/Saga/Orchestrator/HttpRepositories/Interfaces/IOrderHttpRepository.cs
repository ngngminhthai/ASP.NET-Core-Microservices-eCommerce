using Shared.DTOs.Order;

namespace Orchestrator.HttpRepository.Interfaces;

public interface IOrderHttpRepository
{
    Task<long> CreateOrder(OrderDto order);
}