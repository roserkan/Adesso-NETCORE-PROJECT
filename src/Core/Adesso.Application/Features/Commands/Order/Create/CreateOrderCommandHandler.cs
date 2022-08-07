﻿using Adesso.Application.Constants;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using Adesso.Domain.Models;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Commands.Order.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IDataResult<CreateOrderCommand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<CreateOrderCommand>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        IResult result = BusinessRules.Run(
                await CheckUserExist(request.UserId),
                await CheckProductExist(request.CreateOrderItemDtos.Select(i => i.ProductId).ToList()),
                await CheckQuantityProficiencyForProduct(request.CreateOrderItemDtos)

            );
        if (result != null)
        {
            return new ErrorDataResult<CreateOrderCommand>(result.Message);
        }

        var order = GetOrder(request.UserId);

        var orderId = await _unitOfWork.GetRepository<Domain.Models.Order>().AddAsync(order);

        var orderItems = GetOrderItems(orderId, request.CreateOrderItemDtos);
       
        await _unitOfWork.GetRepository<Domain.Models.OrderItem>().BulkAdd(orderItems);

        // İşlemler başarılı ise Siparişin total fiyatı ve ürünün stok adedi güncellenmelidir.
        await UpdateOrderTotal(request.CreateOrderItemDtos);
        await UpdateProductStock(request.CreateOrderItemDtos);


        return new SuccessDataResult<CreateOrderCommand>(null, Messages.OrderSuccess);
    }

    private Domain.Models.Order GetOrder(int userId)
    {
        var order = new Domain.Models.Order()
        {
            OrderDate = DateTime.Now,
            UserId = userId
        };

        return order;
    }


    private List<Domain.Models.OrderItem> GetOrderItems(int orderId, List<CreateOrderItemDto> createOrderItemDto)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in createOrderItemDto)
        {
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    private async Task UpdateOrderTotal(List<CreateOrderItemDto> orderItems)
    {
        decimal total = 0;
        foreach (var orderItem in orderItems)
        {
            var product =  await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(orderItem.ProductId);
            decimal price = (decimal)product.Price;
            decimal quantity = orderItem.Quantity;
            total += price * quantity;
        }

        var order = await _unitOfWork.GetRepository<Domain.Models.Order>().GetByIdAsync(orderItems[0].OrderId);
        order.Total = total;
        await _unitOfWork.GetRepository<Domain.Models.Order>().UpdateAsync(order);
    }

    private async Task UpdateProductStock(List<CreateOrderItemDto> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            var product = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(orderItem.ProductId);
            int newStock = product.Stock - orderItem.Quantity;
            product.Stock = newStock;
            await _unitOfWork.GetRepository<Domain.Models.Product>().UpdateAsync(product);
        }
    }

    // Business Checks

    private async Task<IResult> CheckUserExist(int userId)
    {
        var user = await _unitOfWork.GetRepository<Domain.Models.User>().GetByIdAsync(userId);
        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckOrderExist(List<int> orderIds)
    {
        var order = await _unitOfWork.GetRepository<Domain.Models.Order>().GetByIdAsync(orderIds[0]);
        if (order is null)
        {
            return new ErrorResult(Messages.OrderNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckProductExist(List<int> productIds)
    {
        foreach (var productId in productIds)
        {
            var product = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(productId);
            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
        }
        
        return new SuccessResult();
    }

    private async Task<IResult> CheckQuantityProficiencyForProduct(List<CreateOrderItemDto> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            var product = await _unitOfWork.GetRepository<Domain.Models.Product>().GetByIdAsync(orderItem.ProductId);
            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }
            if (product.Stock < orderItem.Quantity)
            {
                return new ErrorResult(Messages.ProductStockError);

            }
        }
       
        return new SuccessResult();
    }





}