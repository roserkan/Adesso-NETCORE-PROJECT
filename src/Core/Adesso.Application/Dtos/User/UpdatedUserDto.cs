﻿

namespace Adesso.Application.Dtos.User;

public class UpdatedUserDto : IDto
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
}
