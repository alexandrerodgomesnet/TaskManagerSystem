using AutoMapper;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.Users.CreateUser;

public class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, CreateUserResponse>();
    }
}