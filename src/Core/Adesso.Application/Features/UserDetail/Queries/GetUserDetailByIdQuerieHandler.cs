using Adesso.Application.Constants;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.UserDetail.Queries;

public class GetUserDetailByIdQuerieHandler : IRequestHandler<GetUserDetailByIdQuerie, UserDetailDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;

    public GetUserDetailByIdQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();
    }

    public async Task<UserDetailDto> Handle(GetUserDetailByIdQuerie request, CancellationToken cancellationToken)
    {
        var category = await _userDetailRepository.GetByIdAsync(request.Id);

        var result = _mapper.Map<UserDetailDto>(category);

        if (result is null)
            throw new DatabaseValidationException(Messages.UserDetailNotFound);

        return result;
    }
}