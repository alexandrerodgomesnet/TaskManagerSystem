using Bogus;
using FluentAssertions;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Tests.Core.Entities;

public class UserTests
{
    private readonly Faker _faker;

    public UserTests()
    {
        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "Deve criar um usuário válido com dados gerados pelo Bogus")]
    public void Deve_Criar_Usuario_Valido()
    {
        // Arrange
        var nomeEsperado = _faker.Person.FullName;
        var emailEsperado = _faker.Internet.Email();

        // Act
        var user = new User(nomeEsperado, emailEsperado);

        // Assert
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Name.Should().Be(nomeEsperado);
        user.Email.Should().Be(emailEsperado);
    }

    [Fact(DisplayName = "Deve permitir alteração do nome e email do usuário")]
    public void Deve_Permitir_Alterar_Propriedades_Do_Usuario()
    {
        // Arrange
        var user = new User(_faker.Person.FullName, _faker.Internet.Email());
        var novoNome = _faker.Person.FullName;
        var novoEmail = _faker.Internet.Email();

        // Act
        user.Name = novoNome;
        user.Email = novoEmail;

        // Assert
        user.Name.Should().Be(novoNome);
        user.Email.Should().Be(novoEmail);
    }

    [Fact(DisplayName = "Deve gerar IDs diferentes para usuários distintos")]
    public void Deve_Gerar_Ids_Diferentes()
    {
        // Arrange & Act
        var user1 = new User(_faker.Person.FullName, _faker.Internet.Email());
        var user2 = new User(_faker.Person.FullName, _faker.Internet.Email());

        // Assert
        user1.Id.Should().NotBe(user2.Id);
    }

    [Theory(DisplayName = "Deve criar usuário com diferentes combinações de nome e email")]
    [InlineData("João Silva", "joao.silva@email.com")]
    [InlineData("Maria Oliveira", "maria.oliveira@email.com")]
    [InlineData("Carlos Santos", "carlos.santos@email.com")]
    public void Deve_Criar_Usuario_Com_Valores_Parametrizados(string nome, string email)
    {
        // Act
        var user = new User(nome, email);

        // Assert
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Name.Should().Be(nome);
        user.Email.Should().Be(email);
    }
}