using Adesso.Application.Constants;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Role;

public class GetRoleByIdQuerieHandler : IRequestHandler<GetRoleByIdQuerie, RoleDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Role>().GetByIdAsync(request.Id);

        var result = _mapper.Map<RoleDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.RoleNotFound);

        return result;
    }
}