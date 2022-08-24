using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Category.Commands.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryDto>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IGenericRepository<Domain.Models.Category> _categoryRepository;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = _unitOfWork.GetRepository<Domain.Models.Category>();
    }

    public async Task<UpdatedCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        await this.CheckCategoryExsist(request.Id);
        await this.CheckCategoryNameExist(request.Name);

        var category = _mapper.Map<Domain.Models.Category>(request);
        await _categoryRepository.UpdateAsync(category);
        var updatedBrandDto = _mapper.Map<UpdatedCategoryDto>(category);
        return updatedBrandDto;
    }


    private async Task CheckCategoryExsist(int id)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(id);
        if (category is null) throw new BusinessException(Messages.CategoryIdNotFound);
    }

    private async Task CheckCategoryNameExist(string name)
    {
        var category = await _categoryRepository.GetSingleAsync(c => c.Name == name);
        if (category is not null) throw new BusinessException(Messages.CategoryNameAlreadyExist);
    }
}
