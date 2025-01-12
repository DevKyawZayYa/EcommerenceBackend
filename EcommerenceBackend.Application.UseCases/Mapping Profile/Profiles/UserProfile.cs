using AutoMapper;
using EcommerenceBackend.Application.Dto.Users;
using EcommerenceBackend.Application.Domain.Entities;
using EcommerenceBackend.Application.UseCases.Commands.RegisterUser;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, RegisterUserCommand>();
        CreateMap<RegisterUserCommand, User>();
    }
}
