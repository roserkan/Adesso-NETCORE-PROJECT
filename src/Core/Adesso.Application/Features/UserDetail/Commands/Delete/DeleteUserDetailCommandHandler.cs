using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.UserDetail.Commands.Delete;

public class DeleteUserDetailCommandHandler : IRequestHandler<DeleteUserDetailCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.UserDetail> _userDetailRepository;


    public DeleteUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userDetailRepository = _unitOfWork.GetRepository<Domain.Models.UserDetail>();

    }

    public async Task<string> Handle(DeleteUserDetailCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckUserDetailExist(request.Id));

        var category = _mapper.Map<Domain.Models.UserDetail>(request);

        var rows = await _userDetailRepository.DeleteAsync(category);

        return Messages.UserDetailDeleted;
    }

    private async Task<IResult> CheckUserDetailExist(int id)
    {
        var user = await _userDetailRepository.GetByIdAsync(id);
        if (user is null)
        {
            return new ErrorResult(Messages.UserDetailNotFound);
        }
        return new SuccessResult();
    }




}
