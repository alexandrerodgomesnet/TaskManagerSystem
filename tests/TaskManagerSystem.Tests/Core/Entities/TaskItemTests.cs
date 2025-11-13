using Bogus;
using FluentAssertions;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Tests.Core.Entities;

public class TaskItemTests
{
    private readonly Faker _faker;
    public TaskItemTests()
    {
        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "Deve criar uma TaskItem válida com dados padrão")]
    public void Deve_Criar_TaskItem_Valida()
    {
        // Arrange
        var title = _faker.Lorem.Sentence(3);
        var description = _faker.Lorem.Paragraph();
        var userId = _faker.Random.Int(1, 1000);
        var dueDate = DateTime.UtcNow.AddDays(3);

        // Act
        var task = new TaskItem
        {
            Title = title,
            Description = description,
            UserId = userId,
            DueDate = dueDate
        };

        // Assert
        task.Should().NotBeNull();
        task.Id.Should().NotBeEmpty();
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.UserId.Should().Be(userId);
        task.IsCompleted.Should().BeFalse();
        task.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
        task.DueDate.Should().BeAfter(task.CreatedAt);
    }

    [Fact(DisplayName = "Deve permitir marcar a tarefa como concluída")]
    public void Deve_Marcar_Como_Concluida()
    {
        // Arrange
        var task = new TaskItem
        {
            Title = _faker.Lorem.Sentence(),
            Description = _faker.Lorem.Paragraph(),
            UserId = _faker.Random.Int(1, 100)
        };

        // Act
        task.MarkAsCompleted();

        // Assert
        task.IsCompleted.Should().BeTrue();
    }

    [Fact(DisplayName = "Deve atualizar os campos corretamente")]
    public void Deve_Atualizar_Campos()
    {
        // Arrange
        var task = new TaskItem
        {
            Title = "Título antigo",
            Description = "Descrição antiga",
            UserId = 1,
            DueDate = DateTime.UtcNow.AddDays(2)
        };

        var novoTitulo = "Novo título";
        var novaDescricao = "Nova descrição";
        var novaData = DateTime.UtcNow.AddDays(5);

        // Act
        task.Title = novoTitulo;
        task.Description = novaDescricao;
        task.DueDate = novaData;

        // Assert
        task.Title.Should().Be(novoTitulo);
        task.Description.Should().Be(novaDescricao);
        task.DueDate.Should().Be(novaData);
    }

    [Fact(DisplayName = "Deve gerar IDs únicos para diferentes instâncias")]
    public void Deve_Gerar_IDs_Unicos()
    {
        // Arrange & Act
        var task1 = new TaskItem { Title = "Tarefa 1" };
        var task2 = new TaskItem { Title = "Tarefa 2" };

        // Assert
        task1.Id.Should().NotBe(task2.Id);
    }
}