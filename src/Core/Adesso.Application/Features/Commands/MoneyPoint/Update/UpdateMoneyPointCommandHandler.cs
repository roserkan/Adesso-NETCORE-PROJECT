using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.MoneyPoint.Update;

public class UpdateMoneyPointCommandHandler : IRequestHandler<UpdateMoneyPointCommand, IDataResult<UpdateMoneyPointCommand>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMoneyPointCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<UpdateMoneyPointCommand>> Handle(UpdateMoneyPointCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckMoneyPointExist(request.Id),
                await CheckCategoryExist(request.CategoryId)
            );
        if (result != null)
        {
            return new ErrorDataResult<UpdateMoneyPointCommand>(result.Message);
        }

        var product = _mapper.Map<Domain.Models.MoneyPoint>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().UpdateAsync(product);

        return new SuccessDataResult<UpdateMoneyPointCommand>(request, Messages.MoneyPointUpdated);
    }


    private async Task<IResult> CheckMoneyPointExist(int id)
    {
        var moneyPoint = await _unitOfWork.GetRepository<Domain.Models.MoneyPoint>().GetByIdAsync(id);
        if (moneyPoint is null)
        {
            return new ErrorResult(Messages.MoneyPointNotFound);
        }
        return new SuccessResult();
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
