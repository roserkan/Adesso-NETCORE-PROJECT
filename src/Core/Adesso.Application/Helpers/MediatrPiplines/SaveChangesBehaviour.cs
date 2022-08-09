using Adesso.Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Adesso.Application.Helpers.MediatrPiplines;

public class SaveChangesBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _uniOfWork;

    public SaveChangesBehaviour(IUnitOfWork uniOfWork)
    {
        _uniOfWork = uniOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();
        _uniOfWork.SaveChanges();
        return response;
    }
}
