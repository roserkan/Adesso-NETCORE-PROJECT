using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.User.Queries;

public class GetAllUsersQuerieHandler : IRequestHandler<GetAllUsersQuerie, List<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.User> _userRepository;


    public GetAllUsersQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _userRepository.GetAll();

        var result = _mapper.Map<List<UserDto>>(categories);

        return result;
    }
}