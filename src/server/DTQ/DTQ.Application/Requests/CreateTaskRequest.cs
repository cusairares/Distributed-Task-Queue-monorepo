namespace DTQ.Application.Requests
{
    public record CreateTaskRequest(string Name, string TaskType, string Payload, int MaxRetries);
}
