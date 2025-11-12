using AutoMapper;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.TaskItems.UpdateTaskItem;

public class UpdateTaskItemProfile : Profile
{
    public UpdateTaskItemProfile()
    {
        CreateMap<UpdateTaskItemRequest, TaskItem>();
        CreateMap<TaskItem, UpdateTaskItemResponse>();
    }
}