using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Application.Interfaces.Services;

namespace TaskManagerSystem.Application.Features.Users;

public class UserService(IUnitOfWork uow) : IUserService
{
}