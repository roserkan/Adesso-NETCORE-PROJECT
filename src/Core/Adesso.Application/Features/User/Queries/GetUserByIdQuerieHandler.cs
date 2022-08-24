using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.User.Queries;

public class GetUserByIdQuerieHandler : IRequestHandler<GetUserByIdQuerie, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.User> _userRepository;


    public GetUserByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();
    }

    public async Task<UserDto> Handle(GetUserByIdQuerie request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        var result = _mapper.Map<UserDto>(user);

        if (result is null)
            throw new BusinessException(Messages.UserNotFound);

        return result;
    }
}