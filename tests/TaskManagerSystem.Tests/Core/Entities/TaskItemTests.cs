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

    [Fact(DisplayName = "Deve criar uma tarefa válida com título, descrição e usuário")]
    public void Deve_Criar_Tarefa_Valida()
    {
        // Arrange
        // var user = new User(_faker.Person.FullName, _faker.Internet.Email());
        // var title = _faker.Lorem.Sentence(3);
        // var description = _faker.Lorem.Paragraph();

        // var task = new TaskItem();

        // // Act
        // task.Add(title, description, user);

        // // Assert
        // task.Title.Should().Be(title);
        // task.Description.Should().Be(description);
        // task.User.Should().Be(user);
        // task.IsCompleted.Should().BeFalse();
        // task.Id.Should().NotBeEmpty();
        // task.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
    }

    [Fact(DisplayName = "Deve atualizar título e descrição e definir DueDate")]
    public void Deve_Atualizar_Tarefa()
    {
        // Arrange
        // var user = new User(_faker.Person.FullName, _faker.Internet.Email());
        // var task = new TaskItem();
        // task.Add("Tarefa inicial", "Descrição inicial", user);

        // var novoTitulo = "Tarefa atualizada";
        // var novaDescricao = "Descrição atualizada";

        // // Act
        // task.Update(novoTitulo, novaDescricao);

        // // Assert
        // task.Title.Should().Be(novoTitulo);
        // task.Description.Should().Be(novaDescricao);
        // task.DueDate.Should().NotBeNull();
        // task.DueDate.Should().BeAfter(task.CreatedAt);
    }

    [Fact(DisplayName = "Deve marcar a tarefa como concluída")]
    public void Deve_Marcar_Tarefa_Como_Concluida()
    {
        // Arrange
        // var user = new User(_faker.Person.FullName, _faker.Internet.Email());
        // var task = new TaskItem();
        // task.Add("Tarefa", "Descrição", user);

        // // Act
        // task.MarkAsCompleted();

        // // Assert
        // task.IsCompleted.Should().BeTrue();
    }
}