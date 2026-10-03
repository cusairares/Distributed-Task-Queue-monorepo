namespace DTQ.Application.Requests
{
    public record UpdateTaskRequest(int FencingToken, string? ErrorMessage = null);
}
