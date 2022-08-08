using Adesso.Application.Constants;
using Adesso.Application.Dtos.User;
using Adesso.Application.Features.Queries.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.User;

public class GetUserByIdQuerieHandler : IRequestHandler<GetUserByIdQuerie, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.User>().GetByIdAsync(request.Id);

        var result = _mapper.Map<UserDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.UserNotFound);

        return result;
    }
}