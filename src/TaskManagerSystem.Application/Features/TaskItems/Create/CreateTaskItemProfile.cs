using AutoMapper;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Application.Features.TaskItems.Create;

public class CreateTaskItemProfile : Profile
{
    public CreateTaskItemProfile()
    {
        CreateMap<CreateTaskItemRequest, TaskItem>();
        CreateMap<TaskItem, CreateTaskItemResponse>();
    }
}