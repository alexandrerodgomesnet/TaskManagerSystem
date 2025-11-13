using TaskManagerSystem.Api.Configurations;
using TaskManagerSystem.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TaskManagerSystemAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.TaskItemsRoutes();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }