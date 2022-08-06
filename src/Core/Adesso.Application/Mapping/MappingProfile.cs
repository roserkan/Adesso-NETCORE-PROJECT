using Adesso.Application.Dtos.Category;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Dtos.Order;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Dtos.User;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Features.Commands.Category.Create;
using Adesso.Application.Features.Commands.Category.Delete;
using Adesso.Application.Features.Commands.Category.Update;
using Adesso.Application.Features.Commands.Product.Create;
using Adesso.Application.Features.Commands.Product.Delete;
using Adesso.Application.Features.Commands.Product.Update;
using Adesso.Domain.Models;
using AutoMapper;


namespace Adesso.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, DeleteCategoryCommand>().ReverseMap();



        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Product, DeleteProductCommand>().ReverseMap();




        CreateMap<User, UserDto>().ReverseMap();
        //CreateMap<User, CreateUserCommand>().ReverseMap();
        //CreateMap<User, UpdateUserCommand>().ReverseMap();
        //CreateMap<User, DeleteUserCommand>().ReverseMap();

        CreateMap<UserDetail, UserDetailDto>().ReverseMap();
        //CreateMap<UserDetail, CreateUserDetailCommand>().ReverseMap();
        //CreateMap<UserDetail, UpdateUserDetailCommand>().ReverseMap();
        //CreateMap<UserDetail, DeleteUserDetailCommand>().ReverseMap();


        CreateMap<Order, OrderDto>().ReverseMap();
        //CreateMap<Order, CreateOrderCommand>().ReverseMap();
        //CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        //CreateMap<Order, DeleteOrderCommand>().ReverseMap();

        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        //CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();
        //CreateMap<OrderItem, UpdateOrderItemCommand>().ReverseMap();
        //CreateMap<OrderItem, DeleteOrderItemCommand>().ReverseMap();

        CreateMap<MoneyPoint, MoneyPointDto>().ReverseMap();
        //CreateMap<MoneyPoint, CreateMoneyPointCommand>().ReverseMap();
        //CreateMap<MoneyPoint, UpdateMoneyPointCommand>().ReverseMap();
        //CreateMap<MoneyPoint, DeleteMoneyPointCommand>().ReverseMap();

    }
}