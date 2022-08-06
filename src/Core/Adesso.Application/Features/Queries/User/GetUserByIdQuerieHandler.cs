using Adesso.Application.Constants;
using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.User;

public class GetUserByIdQuerieHandler : IRequestHandler<GetUserByIdQuerie, IDataResult<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<UserDto>> Handle(GetUserByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.User>().GetByIdAsync(request.Id);

        var result = _mapper.Map<UserDto>(category);

        if (result is null)
            return new ErrorDataResult<UserDto>(Messages.UserNotFound);

        return new SuccessDataResult<UserDto>(result);
    }
}