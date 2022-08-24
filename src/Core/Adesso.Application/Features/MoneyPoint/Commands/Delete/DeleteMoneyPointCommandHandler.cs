using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.MoneyPoint.Commands.Delete;

public class DeleteMoneyPointCommandHandler : IRequestHandler<DeleteMoneyPointCommand, DeletedMoneyPointDto>
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

    public async Task<DeletedMoneyPointDto> Handle(DeleteMoneyPointCommand request, CancellationToken cancellationToken)
    {

        await this.CheckMoneyPointExsist(request.Id);

        var moneyPoint = _mapper.Map<Domain.Models.MoneyPoint>(request);
        moneyPoint.IsDeleted = true;
        await _moneyPointRepository.UpdateAsync(moneyPoint);
        var deletedMoneyPointDto = _mapper.Map<DeletedMoneyPointDto>(moneyPoint);
        return deletedMoneyPointDto;
    }

    private async Task CheckMoneyPointExsist(int id)
    {
        var moneyPoint = await _moneyPointRepository.GetByIdAsync(id);
        if (moneyPoint is null) throw new BusinessException(Messages.MoneyPointNotFound);
    }
}
