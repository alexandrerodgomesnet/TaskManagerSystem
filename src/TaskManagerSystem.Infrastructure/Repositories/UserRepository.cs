using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Infrastructure.Persistence.Context;
using TaskManagerSystem.Infrastructure.Repositories.Base;

namespace TaskManagerSystem.Infrastructure.Repositories;

public class UserRepository(TaskManagerContext context) 
    : RepositoryBase<User>(context), IUserRepository
{


}