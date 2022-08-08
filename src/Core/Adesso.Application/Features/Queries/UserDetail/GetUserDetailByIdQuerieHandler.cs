using Adesso.Application.Constants;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.UserDetail;

public class GetUserDetailByIdQuerieHandler : IRequestHandler<GetUserDetailByIdQuerie, UserDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserDetailByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserDetailDto> Handle(GetUserDetailByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().GetByIdAsync(request.Id);

        var result = _mapper.Map<UserDetailDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.UserDetailNotFound);

        return result;
    }
}