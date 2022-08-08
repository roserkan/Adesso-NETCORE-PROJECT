﻿using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Queries.UserDetail;

public class GetAllUserDetailsQuerieHandler : IRequestHandler<GetAllUserDetailsQuerie, List<UserDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;

    public GetAllUserDetailsQuerieHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();
    }

    public async Task<List<UserDetailDto>> Handle(GetAllUserDetailsQuerie request, CancellationToken cancellationToken)
    {
        var categories = await _userDetailRepository.GetAll();

        var result = _mapper.Map<List<UserDetailDto>>(categories);

        return result;
    }
}