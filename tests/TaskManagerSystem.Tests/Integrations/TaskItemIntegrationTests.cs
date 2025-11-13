using System.Net;
using System.Net.Http.Json;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Infrastructure.Persistence.Context;
using TaskManagerSystem.Application.Features.TaskItems.Create;
using TaskManagerSystem.Core.Entities;

namespace TaskManagerSystem.Tests.Integrations;

public class TaskItemIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Faker _faker;

    public TaskItemIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.FirstOrDefault(d => 
                    d.ServiceType == typeof(DbContextOptions<TaskManagerContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<TaskManagerContext>(options =>
                    options.UseInMemoryDatabase("TaskIntegrationTestDb"));
            });
        }).CreateClient();

        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "POST /api/tasks deve criar uma nova tarefa")]
    public async Task Post_Deve_Criar_Tarefa()
    {
        // Arrange
        var request = new CreateTaskItemRequest(_faker.Lorem.Sentence(3),
            _faker.Lorem.Paragraph(), _faker.Date.Future(), _faker.Random.Int(1, 1000));

        // Act
        var response = await _client.PostAsJsonAsync("/api/tasks", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = await response.Content.ReadFromJsonAsync<CreateTaskItemResponse>();
        result.Should().NotBeNull();
        result!.Title.Should().Be(request.Title);
        result.Description.Should().Be(request.Description);
    }

    [Fact(DisplayName = "GET /api/tasks/{userId} deve retornar tarefas do usuário")]
    public async Task Get_Deve_Listar_Tarefas_Do_Usuario()
    {
        // Arrange
        var userId = _faker.Random.Int(1, 100);
        var request = new CreateTaskItemRequest(_faker.Lorem.Sentence(3),
            _faker.Lorem.Paragraph(), _faker.Date.Future(), userId);

        await _client.PostAsJsonAsync("/api/tasks", request);

        // Act
        var response = await _client.GetAsync($"/api/tasks/{userId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var tasks = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItem>>();
        tasks.Should().NotBeNull();
        tasks.Should().Contain(t => t.UserId == userId);
    }

    [Fact(DisplayName = "PUT /api/tasks/{id}/complete deve marcar como concluída")]
    public async Task Put_Deve_Marcar_Tarefa_Como_Concluida()
    {
        // Arrange
        var userId = 1;
        var request = new CreateTaskItemRequest(_faker.Lorem.Sentence(3),
            _faker.Lorem.Paragraph(), _faker.Date.Future(), userId);

        var createResponse = await _client.PostAsJsonAsync("/api/tasks", request);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskItemResponse>();
        created.Should().NotBeNull();

        // Act
        var response = await _client.PutAsync($"/api/tasks/{created!.Id}/complete", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact(DisplayName = "DELETE /api/tasks/{id} deve remover a tarefa")]
    public async Task Delete_Deve_Remover_Tarefa()
    {
        // Arrange
        var userId = 1;
        var request = new CreateTaskItemRequest(_faker.Lorem.Sentence(3),
            _faker.Lorem.Paragraph(), _faker.Date.Future(), userId);

        var createResponse = await _client.PostAsJsonAsync("/api/tasks", request);
        var created = await createResponse.Content.ReadFromJsonAsync<CreateTaskItemResponse>();
        created.Should().NotBeNull();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/tasks/{created!.Id}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}