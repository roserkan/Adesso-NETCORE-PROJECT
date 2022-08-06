using Adesso.Application.Utilities.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adesso.Application.Features.Commands.Category.Delete;

public class DeleteCategoryCommand: IRequest<IDataResult<DeleteCategoryCommand>>
{
    public DeleteCategoryCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
