using MediatR;
using Shared.SeedWork;

namespace Product.Application.Features.ProductItems.Commands.UpdateProductItem
{
    public class UpdateProductItemComandHandler : IRequestHandler<UpdateProductItemCommand, ApiResult<long>>
    {
        Task<ApiResult<long>> IRequestHandler<UpdateProductItemCommand, ApiResult<long>>.Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
