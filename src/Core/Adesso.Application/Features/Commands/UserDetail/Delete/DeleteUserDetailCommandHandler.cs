using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Commands.UserDetail.Delete;

public class DeleteUserDetailCommandHandler : IRequestHandler<DeleteUserDetailCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserDetailCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(DeleteUserDetailCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(await CheckUserDetailExist(request.Id));

        var category = _mapper.Map<Domain.Models.UserDetail>(request);

        var rows = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().DeleteAsync(category);

        return Messages.UserDetailDeleted;
    }

    private async Task<IResult> CheckUserDetailExist(int id)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.UserDetail>().GetByIdAsync(id);
        if (user is null)
        {
            return new ErrorResult(Messages.UserDetailNotFound);
        }
        return new SuccessResult();
    }




}
