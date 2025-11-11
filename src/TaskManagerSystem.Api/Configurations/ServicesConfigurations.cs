namespace TaskManagerSystem.Api.Configurations;

public static class ServicesConfigurations
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "TaskManagerSystemAPI";
            config.Title = "TaskManagerSystemAPI v1";
            config.Version = "v1";
        });
        
        // builder.Services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1",
        //         new() { Title = "Tarefa Api", Version = "v1" });
        // });
    }
}