using FluentValidation;
using TaskManagerSystem.Application.Features.TaskItems;
using TaskManagerSystem.Application.Features.Users;
using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Application.Interfaces.Repositories.Base;
using TaskManagerSystem.Application.Interfaces.Services;
using TaskManagerSystem.Core;
using TaskManagerSystem.Infrastructure.Repositories;
using TaskManagerSystem.Infrastructure.Repositories.Base;

namespace TaskManagerSystem.Api.Configurations;

public static class ServicesConfigurations
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITaskItemService, TaskItemService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "TaskManagerSystemAPI";
            config.Title = "TaskManagerSystemAPI v1";
            config.Version = "v1";
        });

        builder.Services.AddValidatorsFromAssemblies([
            typeof(Program).Assembly, 
            typeof(CoreLayer).Assembly
        ], includeInternalTypes: true);

        // builder.Services.AddFluentValidationAutoValidation()
        //     .AddValidatorsFromAssembly();
                //.AddValidatorsFromAssemblyContaining<UserValidator>();

        // builder.Services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1",
        //         new() { Title = "Tarefa Api", Version = "v1" });
        // });
    }
}