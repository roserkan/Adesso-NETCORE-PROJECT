using Adesso.Application.Constants;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.Role;

public class GetRoleByIdQuerieHandler : IRequestHandler<GetRoleByIdQuerie, IDataResult<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoleByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<RoleDto>> Handle(GetRoleByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Role>().GetByIdAsync(request.Id);

        var result = _mapper.Map<RoleDto>(category);

        if (result is null)
            return new ErrorDataResult<RoleDto>(Messages.RoleNotFound);

        return new SuccessDataResult<RoleDto>(result);
    }
}