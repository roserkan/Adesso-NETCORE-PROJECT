using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Category.Commands.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeletedCategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Category> _categoryRepository;


    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = _unitOfWork.GetRepository<Domain.Models.Category>();
    }

    public async Task<DeletedCategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await this.CheckCategoryExsist(request.Id);
       

        var category = _mapper.Map<Domain.Models.Category>(request);
        category.IsDeleted = true;
        await _categoryRepository.UpdateAsync(category);
        var deletedCategoryDto = _mapper.Map<DeletedCategoryDto>(category);
        return deletedCategoryDto;
    }

    private async Task CheckCategoryExsist(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null) throw new BusinessException(Messages.CategoryIdNotFound);
    }

}
