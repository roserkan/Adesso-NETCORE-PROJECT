﻿using Adesso.Application.Constants;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;


namespace Adesso.Application.Features.Category.Commands.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
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

    public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckCategoryExsist(request.Id),
                await CheckCategoryNameExist(request.Name)
            );

        var category = _mapper.Map<Domain.Models.Category>(request);

        var rows = await _categoryRepository.UpdateAsync(category);

        return Messages.CategoryUpdated;

    }


    private async Task<IResult> CheckCategoryExsist(int id)
    {
        var category = await _unitOfWork.GetRepository<Domain.Models.Category>().GetByIdAsync(id);
        if (category is null)
        {
            return new ErrorResult(Messages.CategoryIdNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckCategoryNameExsist(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return new ErrorResult(Messages.CategoryIdNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckCategoryNameExist(string name)
    {
        var category = await _categoryRepository.GetSingleAsync(c => c.Name == name);
        if (category is not null)
        {
            return new ErrorResult(Messages.CategoryNameAlreadyExist);
        }
        return new SuccessResult();
    }
}