using Adesso.Application.Interfaces.Repositories;
using MediatR;

namespace Adesso.Application.Pipelines.SaveChanges;

public class SaveChangesBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public SaveChangesBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();
        await _unitOfWork.SaveChangesAsync();
        return response;
    }
}