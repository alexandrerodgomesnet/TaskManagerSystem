using FluentValidation;
using TaskManagerSystem.Application;
using TaskManagerSystem.Application.Features.TaskItems;
using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Application.Interfaces.Repositories.Base;
using TaskManagerSystem.Application.Interfaces.Services;
using TaskManagerSystem.Infrastructure.Repositories;
using TaskManagerSystem.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Infrastructure.Persistence.Context;

namespace TaskManagerSystem.Api.Configurations;

public static class ServicesConfigurations
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TaskManagerContext>(opt =>
            opt.UseInMemoryDatabase("TaskManagerDb"));

        builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITaskItemService, TaskItemService>();

        builder.Services.AddAutoMapperFromAssemblies();
        builder.Services.AddFromAssembliesValidators();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "TaskManagerSystemAPI";
            config.Title = "TaskManagerSystemAPI v1";
            config.Version = "v1";
        });
    }

    private static IServiceCollection AddFromAssembliesValidators(this IServiceCollection services) =>
            services.AddValidatorsFromAssemblies([
                typeof(Program).Assembly, 
                typeof(ApplicationLayer).Assembly
            ], includeInternalTypes: true);

    private static IServiceCollection AddAutoMapperFromAssemblies(this IServiceCollection services) =>
        services.AddAutoMapper(
            typeof(Program).Assembly, 
            typeof(ApplicationLayer).Assembly
        );
}