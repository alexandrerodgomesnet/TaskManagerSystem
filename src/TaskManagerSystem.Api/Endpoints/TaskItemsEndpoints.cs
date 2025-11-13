using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Application.Features.TaskItems.Create;
using TaskManagerSystem.Application.Interfaces.Services;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Api.Endpoints;

public static class TaskItemsEndpoints
{
    public static void TaskItemsRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/tasks")
            .WithTags(nameof(TaskItem))
            .WithOpenApi();

        group.MapPost("", async Task<Results<Created<CreateTaskItemResponse>, BadRequest<string>>> 
            ([FromBody] CreateTaskItemRequest req, ITaskItemService service, CancellationToken cancellationToken) =>

            {
                try
                {
                    var response = await service.CreateAsync(req, cancellationToken);
                    return TypedResults.Created($"/api/tasks/{response.UserId}", response);
                }
                catch (Exception ex)
                {
                    return TypedResults.BadRequest(ex.Message);
                }
                
            })
        .WithName("Create");

        group.MapGet("/{userId:int}", async Task<Results<Ok<IEnumerable<TaskItem>>, NotFound>>
            (int userId, ITaskItemService service) =>
                await service.ListTasksTasksByUser(userId) is IEnumerable<TaskItem> model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound()
            ).WithName("GetByUserId");

        group.MapPut("/{id:guid}/complete", async Task<Results<Ok, NotFound>>
            (Guid id, ITaskItemService service, CancellationToken cancellationToken) =>
        {
            var affected = await service.CompleteTaskAsync(id, cancellationToken);

            return affected ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("Complete");

        group.MapDelete("/{id:guid}", async Task<Results<Ok, NotFound>>
            (Guid id, ITaskItemService service, CancellationToken cancellationToken) =>
                await service.DeleteAsync(id, cancellationToken)
                        ? TypedResults.Ok()
                        : TypedResults.NotFound()
                )
                .WithName("Delete");
    }
}