using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using TaskManagerSystem.Application.Features.TaskItems;
using TaskManagerSystem.Application.Features.TaskItems.Create;
using TaskManagerSystem.Application.Interfaces.Repositories;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Tests.Services;

public class TaskItemServiceTests
{
    private readonly Faker _faker;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<ITaskItemRepository> _taskRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TaskItemService _service;

    public TaskItemServiceTests()
    {
        _faker = new Faker("pt_BR");
        _uowMock = new Mock<IUnitOfWork>();
        _taskRepoMock = new Mock<ITaskItemRepository>();
        _mapperMock = new Mock<IMapper>();

        // Configura o UnitOfWork para retornar o repositório mockado
        _uowMock.Setup(u => u.TaskItems).Returns(_taskRepoMock.Object);

        _service = new TaskItemService(_uowMock.Object, _mapperMock.Object);
    }

    [Fact(DisplayName = "Deve criar uma tarefa com sucesso")]
    public async Task Deve_Criar_Task_Com_Sucesso()
    {
        // Arrange
        var request = new CreateTaskItemRequest(_faker.Lorem.Sentence(3),
            _faker.Lorem.Paragraph(), _faker.Date.Future(), _faker.Random.Int(1, 1000));

        var entity = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description!,
            UserId = request.UserId
        };

        var response = new CreateTaskItemResponse(entity.Id, entity.Title, entity.Description,
            entity.CreatedAt, entity.DueDate, entity.UserId, entity.IsCompleted);

        _mapperMock.Setup(m => m.Map<TaskItem>(It.IsAny<CreateTaskItemRequest>()))
                    .Returns(entity);

        _mapperMock.Setup(m => m.Map<CreateTaskItemResponse>(It.IsAny<TaskItem>()))
                    .Returns(response);

        _uowMock.Setup(u => u.TaskItems.AddAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

        _uowMock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

        // Act
        var result = await _service.CreateAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
        result.Title.Should().Be(request.Title);

        _taskRepoMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>(), It.IsAny<CancellationToken>()), Times.Once);
        _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Deve listar tarefas de um usuário com sucesso")]
    public async Task Deve_Listar_Tarefas_Por_Usuario()
    {
        // Arrange
        var userId = _faker.Random.Int(1, 100);
        var tasks = new List<TaskItem>
        {
            new TaskItem { Id = Guid.NewGuid(), Title = "Tarefa 1", UserId = userId },
            new TaskItem { Id = Guid.NewGuid(), Title = "Tarefa 2", UserId = userId }
        };

        _taskRepoMock.Setup(r => r.ListTasksTasksByUser(userId))
                        .ReturnsAsync(tasks);

        _mapperMock.Setup(m => m.Map<IEnumerable<TaskItem>>(It.IsAny<IEnumerable<TaskItem>>()))
                    .Returns(tasks);

        // Act
        var result = await _service.ListTasksTasksByUser(userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().OnlyContain(t => t.UserId == userId);

        _taskRepoMock.Verify(r => r.ListTasksTasksByUser(userId), Times.Once);
    }

    [Fact(DisplayName = "Deve marcar uma tarefa como concluída com sucesso")]
    public async Task Deve_Marcar_Como_Concluida()
    {
        // Arrange
        var id = Guid.NewGuid();

        _taskRepoMock.Setup(r => r.MarkAsCompleted(id))
                        .Returns(Task.CompletedTask);

        _uowMock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

        // Act
        var result = await _service.CompleteTaskAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        _taskRepoMock.Verify(r => r.MarkAsCompleted(id), Times.Once);
        _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Deve deletar uma tarefa com sucesso")]
    public async Task Deve_Deletar_Tarefa_Com_Sucesso()
    {
        // Arrange
        var id = Guid.NewGuid();
        var task = new TaskItem { Id = id, Title = "Teste" };

        _taskRepoMock.Setup(r => r.GetByIdAsync(id))
                        .ReturnsAsync(task);

        _taskRepoMock.Setup(r => r.DeleteAsync(task))
                        .Returns(Task.CompletedTask);

        _uowMock.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(id, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        _taskRepoMock.Verify(r => r.GetByIdAsync(id), Times.Once);
        _taskRepoMock.Verify(r => r.DeleteAsync(task), Times.Once);
        _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}