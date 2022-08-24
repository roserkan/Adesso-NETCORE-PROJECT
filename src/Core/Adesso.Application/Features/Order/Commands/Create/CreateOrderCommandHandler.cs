using Adesso.Application.Constants;
using Adesso.Application.Dtos.Order;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Interfaces.Repositories;
using Adesso.Application.Utilities.Business;
using Adesso.Application.Utilities.Results;
using AutoMapper;
using MediatR;

namespace Adesso.Application.Features.Order.Commands.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IGenericRepository<Domain.Models.Order> _orderRepository;
    private IGenericRepository<Domain.Models.OrderItem> _orderItemRepository;
    private IGenericRepository<Domain.Models.Product> _productRepository;
    private IGenericRepository<Domain.Models.User> _userRepository;


    public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderRepository = _unitOfWork.GetRepository<Domain.Models.Order>();
        _orderItemRepository = _unitOfWork.GetRepository<Domain.Models.OrderItem>();
        _productRepository = _unitOfWork.GetRepository<Domain.Models.Product>();
        _userRepository = _unitOfWork.GetRepository<Domain.Models.User>();

    }

    public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await this.CheckUserExist(request.UserId);
        await this.CheckProductExist(request.CreateOrderItemDtos.Select(i => i.ProductId).ToList());
        await this.CheckQuantityProficiencyForProduct(request.CreateOrderItemDtos);

        var order = GetOrder(request.UserId);

        var orderId = await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        var orderItems = GetOrderItems(orderId, request.CreateOrderItemDtos);
       
        await _orderItemRepository.BulkAdd(orderItems);

        // İşlemler başarılı ise Siparişin total fiyatı ve ürünün stok adedi güncellenmelidir.
        await this.UpdateOrderTotal(request.CreateOrderItemDtos);
        await this.UpdateProductStock(request.CreateOrderItemDtos);

        var createdOrderDto = _mapper.Map<CreatedOrderDto>(order);
        return createdOrderDto;
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
        var orderItems = new List<Domain.Models.OrderItem>();

        foreach (var item in createOrderItemDto)
        {
            var orderItem = new Domain.Models.OrderItem
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
            var product =  await _productRepository.GetByIdAsync(orderItem.ProductId);
            decimal price = (decimal)product.Price;
            decimal quantity = orderItem.Quantity;
            total += price * quantity;
        }

        var order = await _orderRepository.GetByIdAsync(orderItems[0].OrderId);
        order.Total = total;
        await _unitOfWork.GetRepository<Domain.Models.Order>().UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task UpdateProductStock(List<CreateOrderItemDto> orderItems)
    {
        foreach (var orderItem in orderItems)
        {
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
            int newStock = product.Stock - orderItem.Quantity;
            product.Stock = newStock;
            await _productRepository.UpdateAsync(product);
        }
    }

    // Business Checks

    private async Task<IResult> CheckUserExist(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return new ErrorResult(Messages.UserNotFound);
        }
        return new SuccessResult();
    }

    private async Task<IResult> CheckOrderExist(List<int> orderIds)
    {
        var order = await _orderRepository.GetByIdAsync(orderIds[0]);
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
            var product = await _productRepository.GetByIdAsync(productId);
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
            var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
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
