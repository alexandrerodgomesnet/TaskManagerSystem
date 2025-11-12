using Bogus;
using FluentAssertions;
using TaskManagerSystem.Core.Entities;
using TaskManagerSystem.Core.Validations;

namespace TaskManagerSystem.Tests.Core.Validations;

public class UserValidatorTests
{
    private readonly Faker _faker;
    private readonly UserValidator _validator;

    public UserValidatorTests()
    {
        _faker = new Faker("pt_BR");
        _validator = new UserValidator();
    }

    [Fact(DisplayName = "Deve validar usuário com dados válidos")]
    public void Deve_Validar_Usuario_Valido()
    {
        // Arrange
        var user = new User(_faker.Person.FullName, _faker.Internet.Email());

        // Act
        var result = _validator.Validate(user);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory(DisplayName = "Deve invalidar usuário com nome ou e-mail inválidos")]
    [InlineData("", "email@email.com")]
    [InlineData(null, "email@email.com")]
    [InlineData("Nome", "")]
    [InlineData("Nome", null)]
    [InlineData("Nome", "email-invalido")]
    public void Deve_Invalidar_Campos_Invalidos(string? nome, string? email)
    {
        // Arrange
        var user = new User(nome ?? string.Empty, email ?? string.Empty);

        // Act
        var result = _validator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}