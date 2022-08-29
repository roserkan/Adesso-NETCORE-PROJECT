using Adesso.Application.Dtos.Category;
using Adesso.Application.Dtos.MoneyPoint;
using Adesso.Application.Dtos.Order;
using Adesso.Application.Dtos.OrderItem;
using Adesso.Application.Dtos.Product;
using Adesso.Application.Dtos.Role;
using Adesso.Application.Dtos.User;
using Adesso.Application.Dtos.UserDetail;
using Adesso.Application.Features.Category.Commands.Create;
using Adesso.Application.Features.Category.Commands.Delete;
using Adesso.Application.Features.Category.Commands.Update;
using Adesso.Application.Features.MoneyPoint.Commands.Create;
using Adesso.Application.Features.MoneyPoint.Commands.Delete;
using Adesso.Application.Features.MoneyPoint.Commands.Update;
using Adesso.Application.Features.Order.Commands.Create;
using Adesso.Application.Features.Product.Commands.Create;
using Adesso.Application.Features.Product.Commands.Delete;
using Adesso.Application.Features.Product.Commands.Update;
using Adesso.Application.Features.Role.Commands.Create;
using Adesso.Application.Features.Role.Commands.Delete;
using Adesso.Application.Features.Role.Commands.Update;
using Adesso.Application.Features.User.Commands.Create;
using Adesso.Application.Features.User.Commands.Delete;
using Adesso.Application.Features.User.Commands.Update;
using Adesso.Application.Features.UserDetail.Commands.Create;
using Adesso.Application.Features.UserDetail.Commands.Delete;
using Adesso.Application.Features.UserDetail.Commands.Update;
using Adesso.Domain.Models;
using AutoMapper;


namespace Adesso.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        //Categories
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreatedCategoryDto>().ReverseMap();
        CreateMap<Category, UpdatedCategoryDto>().ReverseMap();
        CreateMap<Category, DeletedCategoryDto>().ReverseMap();

        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, DeleteCategoryCommand>().ReverseMap();


        //Products
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, CreatedProductDto>().ReverseMap();
        CreateMap<Product, UpdatedProductDto>().ReverseMap();
        CreateMap<Product, DeletedProductDto>().ReverseMap();

        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Product, DeleteProductCommand>().ReverseMap();


        //Users
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreatedUserDto>().ReverseMap();
        CreateMap<User, UpdatedUserDto>().ReverseMap();
        CreateMap<User, DeletedUserDto>().ReverseMap();
        CreateMap<User, LoginUserDto>().ReverseMap();

        CreateMap<User, CreateUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, DeleteUserCommand>().ReverseMap();


        //UserDetails
        CreateMap<UserDetail, UserDetailDto>().ReverseMap();
        CreateMap<UserDetail, CreatedUserDetailDto>().ReverseMap();
        CreateMap<UserDetail, UpdatedUserDetailDto>().ReverseMap();
        CreateMap<UserDetail, DeletedUserDetailDto>().ReverseMap();

        CreateMap<UserDetail, CreateUserDetailCommand>().ReverseMap();
        CreateMap<UserDetail, UpdateUserDetailCommand>().ReverseMap();
        CreateMap<UserDetail, DeleteUserDetailCommand>().ReverseMap();


        //Orders
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, CreateOrderCommand>().ReverseMap();


        CreateMap<Order, CreateOrderItemDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();


        //MoneyPoints
        CreateMap<MoneyPoint, MoneyPointDto>().ReverseMap();
        CreateMap<MoneyPoint, CreatedMoneyPointDto>().ReverseMap();
        CreateMap<MoneyPoint, UpdatedMoneyPointDto>().ReverseMap();
        CreateMap<MoneyPoint, DeletedMoneyPointDto>().ReverseMap();

        CreateMap<MoneyPoint, CreateMoneyPointCommand>().ReverseMap();
        CreateMap<MoneyPoint, UpdateMoneyPointCommand>().ReverseMap();
        CreateMap<MoneyPoint, DeleteMoneyPointCommand>().ReverseMap();


        //Roles
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Role, CreatedRoleDto>().ReverseMap();
        CreateMap<Role, UpdatedRoleDto>().ReverseMap();
        CreateMap<Role, DeletedRoleDto>().ReverseMap();

        CreateMap<Role, CreateRoleCommand>().ReverseMap();
        CreateMap<Role, UpdateRoleCommand>().ReverseMap();
        CreateMap<Role, DeleteRoleCommand>().ReverseMap();

    }
}