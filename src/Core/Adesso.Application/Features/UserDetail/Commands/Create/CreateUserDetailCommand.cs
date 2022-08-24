using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Enums;
using MediatR;

namespace Adesso.Application.Features.UserDetail.Commands.Create;

public class CreateUserDetailCommand: IRequest<CreatedUserDetailDto>
{
    public CreateUserDetailCommand(int id, int userId, string firstName, string lastName, string address, Genders gender, DateTime createdTime)
    {
        Id = id;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Gender = gender;
        CreatedTime = createdTime;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public Genders Gender { get; set; }
    public DateTime CreatedTime { get; set; }
}
