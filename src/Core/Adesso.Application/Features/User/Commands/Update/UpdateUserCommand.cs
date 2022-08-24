using Adesso.Application.Dtos.User;
using Adesso.Application.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.Features.User.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserDto>
{
    public UpdateUserCommand(int id, string emailAddress, string password)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
    }

    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }


}
