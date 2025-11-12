namespace TaskManagerSystem.Core.Requests.TaskItems;

public record TaskItemRequest(string Title, string Description, DateTime DueDate, Guid UserId);


/*


Cada tarefa deve ter um id, título, descrição, data de criação, data prazo de
conclusão, identificador do usuário vinculado à tarefa e um indicador de tarefa
pendente ou concluída.

*/