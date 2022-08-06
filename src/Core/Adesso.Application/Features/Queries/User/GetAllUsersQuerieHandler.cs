using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.User;

public class GetAllUsersQuerieHandler : IRequestHandler<GetAllUsersQuerie, IDataResult<List<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllUsersQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<List<UserDto>>> Handle(GetAllUsersQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.GetRepository<Domain.Models.User>().GetAll();

        var result = _mapper.Map<List<UserDto>>(categories);

        return new SuccessDataResult<List<UserDto>>(result);
    }
}