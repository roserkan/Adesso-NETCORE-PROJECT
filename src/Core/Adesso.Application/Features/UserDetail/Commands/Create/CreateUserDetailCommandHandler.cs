using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Commands.Create;

public class CreateUserDetailCommandHandler : IRequestHandler<CreateUserDetailCommand, CreatedUserDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public CreateUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();

    }

    public async Task<CreatedUserDetailDto> Handle(CreateUserDetailCommand request, CancellationToken cancellationToken)
    {
        await this.CheckUserExist(request.UserId);
      
        var userDetail = _mapper.Map<Domain.Models.UserDetail>(request);
        await _userDetailRepository.AddAsync(userDetail);
        var createdUserDetailDto = _mapper.Map<CreatedUserDetailDto>(request);
        return createdUserDetailDto;
    }

    private async Task CheckUserExist(int userId)
    {
        var user = await _userRepository
            .GetSingleAsync(u => u.Id == userId);
        var userDetail = await _userDetailRepository
            .GetSingleAsync(u => u.UserId == userId);

        if (user is null) throw new BusinessException(Messages.UserNotFound);

        if (userDetail is not null) throw new BusinessException(Messages.UserDetailAlreadyExist);

    }


}