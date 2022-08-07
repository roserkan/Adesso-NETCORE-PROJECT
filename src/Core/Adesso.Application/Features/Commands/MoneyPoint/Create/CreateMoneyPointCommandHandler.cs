using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.MoneyPoint.Create;

public class CreateMoneyPointCommandHandler : IRequestHandler<CreateMoneyPointCommand, IDataResult<CreateMoneyPointCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMoneyPointCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<CreateMoneyPointCommand>> Handle(CreateMoneyPointCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckCategoryExist(request.CategoryId)

            );
        if (result != null)
        {
            return new ErrorDataResult<CreateMoneyPointCommand>(result.Message);
        }

        var moneyPoint = _mapper.Map<Domain.Models.MoneyPoint>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().AddAsync(moneyPoint);

        return new SuccessDataResult<CreateMoneyPointCommand>(request, Messages.MoneyPointCreated);
    }


    private async Task<IResult> CheckCategoryExist(int categoryId)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(categoryId);
        var moneyPoint = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>()
            .GetSingleAsync(i => i.CategoryId == categoryId);

        if (category is null)
        {
            return new ErrorResult(Messages.CategoryIdNotFound);
        }

        if (moneyPoint is not null)
        {
            return new ErrorResult(Messages.MoneyPointCategoryIdAldreadyExist);

        }


        return new SuccessResult();
    }

   


}
