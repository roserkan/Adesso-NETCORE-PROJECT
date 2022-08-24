using Adesso.Application.Constants;
using Adesso.Application.CrossCuttingConcerns.Exceptions;
using Adesso.Application.Dtos.Category;
using Adesso.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Category.Commands.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Models.Category> _categoryRepository;
    

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _categoryRepository = unitOfWork.GetRepository<Domain.Models.Category>();
    }

    public async Task<CreatedCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {

        await this.CheckCategoryNameExist(request.Name);


        var category = _mapper.Map<Domain.Models.Category>(request);
        await _categoryRepository.AddAsync(category);
        var createdBrandDto = _mapper.Map<CreatedCategoryDto>(category);
        //await _unitOfWork.SaveChangesAsync(); // for init ID
        return createdBrandDto;
            

    }

    private async Task CheckCategoryNameExist(string name)
    {
        var category = await _categoryRepository.GetSingleAsync(c => c.Name == name);
        if (category is not null) throw new BusinessException(Messages.CategoryNameAlreadyExist);
    }

}