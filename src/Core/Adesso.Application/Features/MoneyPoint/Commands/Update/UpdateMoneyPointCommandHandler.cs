using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.MoneyPoint.Commands.Update;

public class UpdateMoneyPointCommandHandler : IRequestHandler<UpdateMoneyPointCommand, DeletedMoneyPointDto>
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

    public async Task<DeletedMoneyPointDto> Handle(UpdateMoneyPointCommand request, CancellationToken cancellationToken)
    {

        await this.CheckMoneyPointExist(request.Id);
        await this.CheckCategoryExist(request.CategoryId);

        var moneyPoint = _mapper.Map<Domain.Models.MoneyPoint>(request);

        await _moneyPointRepository.UpdateAsync(moneyPoint);
        var updatedMoneyPointDto = _mapper.Map<DeletedMoneyPointDto>(moneyPoint);
        return updatedMoneyPointDto;
    }


    private async Task CheckMoneyPointExist(int id)
    {
        var moneyPoint = await _moneyPointRepository.GetByIdAsync(id);
        if (moneyPoint is null) throw new BusinessException(Messages.MoneyPointNotFound);

    }

    private async Task CheckCategoryExist(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        var moneyPoint = await _moneyPointRepository
            .GetSingleAsync(i => i.CategoryId == categoryId);

        if (category is null) throw new BusinessException(Messages.CategoryIdNotFound);

        if (moneyPoint is not null) throw new BusinessException(Messages.MoneyPointCategoryIdAldreadyExist);

    }



}
