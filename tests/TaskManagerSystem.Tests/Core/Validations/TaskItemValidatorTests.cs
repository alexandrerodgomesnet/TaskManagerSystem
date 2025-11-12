using Bogus;
using FluentAssertions;
using FluentValidation.TestHelper;
using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Core.Validations;

namespace TaskManagerSystem.Tests.Core.Validations;

public class TaskItemValidatorTests
{
    private readonly Faker _faker;
    private readonly TaskItemValidator _validator;

    public TaskItemValidatorTests()
    {
        _faker = new Faker("pt_BR");
        _validator = new TaskItemValidator();
    }

    [Fact(DisplayName = "Deve validar uma tarefa válida corretamente")]
    public void Deve_Validar_Tarefa_Valida()
    {
        // Arrange
        var user = new User(_faker.Person.FullName, _faker.Internet.Email());
        var task = new TaskItem
        {
            Title = _faker.Lorem.Sentence(3),
            Description = _faker.Lorem.Sentence(10),
            User = user,
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = _validator.Validate(task);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory(DisplayName = "Deve gerar erro se título for nulo, vazio ou muito curto")]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("A")]
    [InlineData("AB")]
    public void Deve_Gerar_Erro_Titulo_Invalido(string titulo)
    {
        // Arrange
        var task = new TaskItem
        {
            Title = titulo,
            User = new User(_faker.Person.FullName, _faker.Internet.Email())
        };

        // Act
        var result = _validator.TestValidate(task);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.Title);
    }

    [Fact(DisplayName = "Deve gerar erro se usuário for nulo")]
    public void Deve_Gerar_Erro_Usuario_Nulo()
    {
        // Arrange
        var task = new TaskItem
        {
            Title = _faker.Lorem.Sentence(3),
            User = null
        };

        // Act
        var result = _validator.TestValidate(task);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.User);
    }

    [Fact(DisplayName = "Deve gerar erro se descrição for muito longa")]
    public void Deve_Gerar_Erro_Descricao_Muito_Longa()
    {
        // Arrange
        var descricaoLonga = _faker.Lorem.Letter(600);
        var task = new TaskItem
        {
            Title = _faker.Lorem.Sentence(3),
            Description = descricaoLonga,
            User = new User(_faker.Person.FullName, _faker.Internet.Email())
        };

        // Act
        var result = _validator.TestValidate(task);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.Description);
    }

    [Fact(DisplayName = "Deve gerar erro se DueDate for anterior ao CreatedAt")]
    public void Deve_Gerar_Erro_Data_Expiracao_Invalida()
    {
        // Arrange
        var task = new TaskItem
        {
            Title = _faker.Lorem.Sentence(3),
            User = new User(_faker.Person.FullName, _faker.Internet.Email()),
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddHours(-1)
        };

        // Act
        var result = _validator.TestValidate(task);

        // Assert
        result.ShouldHaveValidationErrorFor(t => t.DueDate);
    }
}