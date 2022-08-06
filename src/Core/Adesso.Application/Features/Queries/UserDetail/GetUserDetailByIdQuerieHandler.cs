using Adesso.Application.Constants;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.UserDetail;

public class GetUserDetailByIdQuerieHandler : IRequestHandler<GetUserDetailByIdQuerie, IDataResult<UserDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserDetailByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<UserDetailDto>> Handle(GetUserDetailByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().GetByIdAsync(request.Id);

        var result = _mapper.Map<UserDetailDto>(category);

        if (result is null)
            return new ErrorDataResult<UserDetailDto>(Messages.UserDetailNotFound);

        return new SuccessDataResult<UserDetailDto>(result);
    }
}