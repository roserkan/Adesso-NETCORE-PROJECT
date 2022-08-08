using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.MoneyPoint.Delete;

public class DeleteMoneyPointCommandHandler : IRequestHandler<DeleteMoneyPointCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.MoneyPoint> _moneyPointRepository;


    public DeleteMoneyPointCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _moneyPointRepository = _unitOfWork.GetRepository<Domain.Models.MoneyPoint>();

    }

    public async Task<string> Handle(DeleteMoneyPointCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
            await CheckMoneyPointExsist(request.Id)
            );
        var product = _mapper.Map<Domain.Models.MoneyPoint>(request);

        var rows = await _moneyPointRepository.DeleteAsync(product);

        return Messages.MoneyPointDeleted;
    }

    private async Task<IResult> CheckMoneyPointExsist(int id)
    {
        var product = await _moneyPointRepository.GetByIdAsync(id);
        if (product is null)
        {
            return new ErrorResult(Messages.MoneyPointNotFound);
        }
        return new SuccessResult();
    }
}
