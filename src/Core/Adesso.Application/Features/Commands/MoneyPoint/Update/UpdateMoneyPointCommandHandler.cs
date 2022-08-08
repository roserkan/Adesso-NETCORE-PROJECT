using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.MoneyPoint.Update;

public class UpdateMoneyPointCommandHandler : IRequestHandler<UpdateMoneyPointCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.MoneyPoint> _moneyPointRepository;
    private IGenericRepository<Domain.Models.Category> _categoryRepository;


    public UpdateMoneyPointCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _moneyPointRepository = _unitOfWork.GetRepository<Domain.Models.MoneyPoint>();
        _categoryRepository = _unitOfWork.GetRepository<Domain.Models.Category>();

    }

    public async Task<string> Handle(UpdateMoneyPointCommand request, CancellationToken cancellationToken)
    {

        IResult result = BusinessRules.Run(
                await CheckMoneyPointExist(request.Id),
                await CheckCategoryExist(request.CategoryId)
            );

        var product = _mapper.Map<Domain.Models.MoneyPoint>(request);

        var rows = await _moneyPointRepository.UpdateAsync(product);

        return Messages.MoneyPointUpdated;
    }


    private async Task<IResult> CheckMoneyPointExist(int id)
    {
        var moneyPoint = await _moneyPointRepository.GetByIdAsync(id);
        if (moneyPoint is null)
        {
            return new ErrorResult(Messages.MoneyPointNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckCategoryExist(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        var moneyPoint = await _moneyPointRepository
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
