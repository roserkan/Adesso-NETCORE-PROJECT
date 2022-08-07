using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.MoneyPoint.Delete;

public class DeleteMoneyPointCommandHandler : IRequestHandler<DeleteMoneyPointCommand, IDataResult<DeleteMoneyPointCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMoneyPointCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<DeleteMoneyPointCommand>> Handle(DeleteMoneyPointCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
            await CheckMoneyPointExsist(request.Id)
            );
        if (result != null)
        {
            return new ErrorDataResult<DeleteMoneyPointCommand>(result.Message);
        }

        var product = _mapper.Map<Domain.Models.MoneyPoint>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().DeleteAsync(product);

        return new SuccessDataResult<DeleteMoneyPointCommand>(request, Messages.MoneyPointDeleted);
    }

    private async Task<IResult> CheckMoneyPointExsist(int id)
    {
        var product = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.MoneyPointNotFound);
        }
        return new SuccessResult();
    }
}
