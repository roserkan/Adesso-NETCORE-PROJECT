using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.User;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Results;
using Adesso.Application.Utilities.Security;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.Features.User.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();
    }

    public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await this.CheckEmailAddressExist(request.EmailAddress);

        var user = _mapper.Map<Domain.Models.User>(request);
        user.Password = PasswordEncryptor.Encrypt(user.Password);
        await _userRepository.AddAsync(user);
        var createdUser = _mapper.Map<CreatedUserDto>(user);
        return createdUser;
    }

    private async Task CheckEmailAddressExist(string emailAddress)
    {
        var user = await _userRepository.GetSingleAsync(u => u.EmailAddress == emailAddress);
        if (user is not null) throw new BusinessException(Messages.UserEmailAddressNotAvailable);

    }







}