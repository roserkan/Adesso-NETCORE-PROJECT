﻿using Adesso.Application.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.Features.Commands.Category.Update;

public class UpdateCategoryCommand: IRequest<IDataResult<UpdateCategoryCommand>>
{
    public UpdateCategoryCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}