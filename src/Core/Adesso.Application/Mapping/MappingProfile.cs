using Adesso.Application.Dtos.Category;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Dtos.Order;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Dtos.User;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Features.Commands.Category.Create;
using Adesso.Application.Features.Commands.Category.Delete;
using Adesso.Application.Features.Commands.Category.Update;
using Adesso.Application.Features.Commands.MoneyPoint.Create;
using Adesso.Application.Features.Commands.MoneyPoint.Delete;
using Adesso.Application.Features.Commands.MoneyPoint.Update;
using Adesso.Application.Features.Commands.Order.Create;
using Adesso.Application.Features.Commands.Product.Create;
using Adesso.Application.Features.Commands.Product.Delete;
using Adesso.Application.Features.Commands.Product.Update;
using Adesso.Application.Features.Commands.Role.Create;
using Adesso.Application.Features.Commands.Role.Delete;
using Adesso.Application.Features.Commands.Role.Update;
using Adesso.Application.Features.Commands.User.Create;
using Adesso.Application.Features.Commands.User.Delete;
using Adesso.Application.Features.Commands.User.Update;
using Adesso.Application.Features.Commands.UserDetail.Create;
using Adesso.Application.Features.Commands.UserDetail.Delete;
using Adesso.Application.Features.Commands.UserDetail.Update;
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
        CreateMap<User, LoginUserDto>().ReverseMap();
        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();

        CreateMap<UserDetail, UserDetailDto>().ReverseMap();
        CreateMap<UserDetail, CreateUserDetailCommand>().ReverseMap();
        CreateMap<UserDetail, UpdateUserDetailCommand>().ReverseMap();
        CreateMap<UserDetail, DeleteUserDetailCommand>().ReverseMap();


        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, CreateOrderItemDto>().ReverseMap();
        CreateMap<Order, CreateOrderCommand>().ReverseMap();

        //CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        //CreateMap<Order, DeleteOrderCommand>().ReverseMap();

        CreateMap<OrderItem, OrderItemDto>().ReverseMap();

        //CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();
        //CreateMap<OrderItem, UpdateOrderItemCommand>().ReverseMap();
        //CreateMap<OrderItem, DeleteOrderItemCommand>().ReverseMap();

        CreateMap<MoneyPoint, MoneyPointDto>().ReverseMap();
        CreateMap<MoneyPoint, CreateMoneyPointCommand>().ReverseMap();
        CreateMap<MoneyPoint, UpdateMoneyPointCommand>().ReverseMap();
        CreateMap<MoneyPoint, DeleteMoneyPointCommand>().ReverseMap();


        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Role, CreateRoleCommand>().ReverseMap();
        CreateMap<Role, UpdateRoleCommand>().ReverseMap();
        CreateMap<Role, DeleteRoleCommand>().ReverseMap();

    }
}