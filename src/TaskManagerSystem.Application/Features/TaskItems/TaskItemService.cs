using AutoMapper;
using FluentValidation;
using TaskManagerSystem.Application.Features.TaskItems.Create;
using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Application.Interfaces.Services;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.TaskItems;

public class TaskItemService(IUnitOfWork uow, IMapper mapper) : ITaskItemService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IMapper _mapper = mapper;

    public async Task<CreateTaskItemResponse> CreateAsync(CreateTaskItemRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateTaskItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var taskItem = _mapper.Map<TaskItem>(request);

        await _uow.TaskItems.AddAsync(taskItem, cancellationToken);
        await _uow.CommitAsync(cancellationToken);

        var result = _mapper.Map<CreateTaskItemResponse>(taskItem);
        return result;
    }

    public async Task<IEnumerable<TaskItem>> ListTasksTasksByUser(int userId)
    {
        var response = await _uow.TaskItems.ListTasksTasksByUser(userId);
        var taskItems = _mapper.Map<IEnumerable<TaskItem>>(response);

        return taskItems;
    }
        
        
    public async Task<bool> CompleteTaskAsync(Guid id, CancellationToken cancellationToken)
    {
        await _uow.TaskItems.MarkAsCompleted(id);
        return await _uow.CommitAsync(cancellationToken);
    } 

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var taskItem = await _uow.TaskItems.GetByIdAsync(id);
        await _uow.TaskItems.DeleteAsync(taskItem!);
        return await _uow.CommitAsync(cancellationToken);
    }
}