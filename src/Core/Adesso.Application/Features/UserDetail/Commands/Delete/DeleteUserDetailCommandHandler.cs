using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Commands.Delete;

public class DeleteUserDetailCommandHandler : IRequestHandler<DeleteUserDetailCommand, DeletedUserDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;


    public DeleteUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();

    }

    public async Task<DeletedUserDetailDto> Handle(DeleteUserDetailCommand request, CancellationToken cancellationToken)
    {
        await this.CheckUserDetailExist(request.Id);

        var category = _mapper.Map<Domain.Models.UserDetail>(request);

        await _userDetailRepository.DeleteAsync(category);
        var deletedUserDetailDto = _mapper.Map<DeletedUserDetailDto>(request);
        return deletedUserDetailDto;
    }

    private async Task CheckUserDetailExist(int id)
    {
        var user = await _userDetailRepository.GetByIdAsync(id);
        if (user is null) throw new BusinessException(Messages.UserDetailNotFound);
    }




}
