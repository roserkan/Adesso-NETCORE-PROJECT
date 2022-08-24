using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Commands.Update;

public class UpdateUserDetailCommandHandler : IRequestHandler<UpdateUserDetailCommand, UpdatedUserDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;
    private IGenericRepository<Domain.Models.User> _userRepository;

    public UpdateUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();
    }

    public async Task<UpdatedUserDetailDto> Handle(UpdateUserDetailCommand request, CancellationToken cancellationToken)
    {

        await this.CheckUserDetailExist(request.Id);
        await this.CheckUserExist(request.UserId);

        var userDetail = _mapper.Map<Domain.Models.UserDetail>(request);

        await _userDetailRepository.UpdateAsync(userDetail);
        var updatedUserDetailDto = _mapper.Map<UpdatedUserDetailDto>(request);
        return updatedUserDetailDto;
    }



    private async Task CheckUserExist(int userId)
    {
        var user = await _userRepository
            .GetSingleAsync(u => u.Id == userId);
        

        if (user is null) throw new BusinessException(Messages.UserNotFound);
    }

    private async Task CheckUserDetailExist(int id)
    {
        var userDetail = await _userDetailRepository.GetByIdAsync(id);
        if (userDetail is null) throw new BusinessException(Messages.UserDetailNotFound);
    }

}
