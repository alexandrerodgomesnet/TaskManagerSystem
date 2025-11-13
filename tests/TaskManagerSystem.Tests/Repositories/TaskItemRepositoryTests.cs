using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Infrastructure.Persistence.Context;
using TaskManagerSystem.Infrastructure.Repositories;

namespace TaskManagerSystem.Tests.Repositories;

public class TaskItemRepositoryTests
{
    private readonly Faker _faker;
    private readonly TaskManagerContext _context;
    private readonly TaskItemRepository _repository;

    public TaskItemRepositoryTests()
    {
        _faker = new Faker("pt_BR");
        _context = CriarContextoEmMemoria();

        // Cria o repositório real com o contexto mockado
        _repository = new TaskItemRepository(_context);
    }

    private TaskManagerContext CriarContextoEmMemoria()
    {
        var options = new DbContextOptionsBuilder<TaskManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new TaskManagerContext(options);
        context.Database.EnsureCreated();
        return context;
    }

   [Fact(DisplayName = "Deve listar tarefas de um usuário com sucesso")]
    public async Task Deve_Listar_Tarefas_Por_Usuario()
    {
        // Arrange
        var userId = _faker.Random.Int(1, 1000);

        var fakeTasks = new List<TaskItem>
        {
            new TaskItem { Title = "Tarefa 1", UserId = userId },
            new TaskItem { Title = "Tarefa 2", UserId = userId }
        };

        await _context.Set<TaskItem>().AddRangeAsync(fakeTasks);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.ListTasksTasksByUser(userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.All(t => t.UserId == userId).Should().BeTrue();
    }

    [Fact(DisplayName = "Deve lançar exceção se não encontrar tarefas do usuário")]
    public async Task Deve_Lancar_Excecao_Se_Nao_Encontrar_Tarefas_Do_Usuario()
    {
        // Act
        var act = async () => await _repository.ListTasksTasksByUser(999);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("List Tasks by user not found.");
    }

    [Fact(DisplayName = "Deve marcar uma tarefa como concluída com sucesso")]
    public async Task Deve_Marcar_Tarefa_Como_Concluida()
    {
        // Arrange

        var task = new TaskItem { Title = "Tarefa", IsCompleted = false, UserId = 1 };
        await _context.Set<TaskItem>().AddAsync(task);
        await _context.SaveChangesAsync();

        _context.Entry(task).State = EntityState.Detached;

        // Act
        await _repository.MarkAsCompleted(task.Id);

        // Assert
        var updated = await _context.Set<TaskItem>().FindAsync(task.Id);
        updated!.IsCompleted.Should().BeTrue();
    }

    [Fact(DisplayName = "Deve lançar exceção se tarefa não for encontrada ao tentar concluir")]
    public async Task Deve_Lancar_Excecao_Se_Tarefa_Nao_Encontrada()
    {
        // Act
        var act = async () => await _repository.MarkAsCompleted(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Task item not found.");
    }
}