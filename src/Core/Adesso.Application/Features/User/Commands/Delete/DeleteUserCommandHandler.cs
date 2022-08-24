using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.User.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private IGenericRepository<Domain.Models.User> _userRepository;

    public DeleteUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();

    }

    public async Task<DeletedUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await this.CheckUserExist(request.Id);

        var user = _mapper.Map<Domain.Models.User>(request);

        await _userRepository.DeleteAsync(user);
        var deletedUser = _mapper.Map<DeletedUserDto>(user);
        return deletedUser;
    }

    private async Task CheckUserExist(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) throw new BusinessException(Messages.UserNotFound);

    }




}
