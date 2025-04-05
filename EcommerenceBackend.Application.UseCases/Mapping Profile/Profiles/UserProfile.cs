using AutoMapper;
using EcommerenceBackend.Application.Domain.Users;
using EcommerenceBackend.Application.Dto.Users;
using EcommerenceBackend.Application.UseCases.Onboarding.Commands.RegisterUser;
using EcommerenceBackend.Application.UseCases.User.Queries.GetUserProfileByAllQuery;
using EcommerenceBackend.Application.UseCases.Users.Commands.UpdateUserProfileById;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterUserDto, RegisterUserCommand>();
        CreateMap<RegisterUserCommand, User>();

        CreateMap<User, UserProfileDto>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src!.Id.Value))
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName!.Value))
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName!.Value))
          ;

        CreateMap<UpdateUserProfileByIdCommand, User>()
          .ForMember(dest => dest.Id, opt => opt.Ignore()); 

    }
}
