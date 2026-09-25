namespace DTQ.Application.Requests
{
    public record UpdateTaskRequest(Guid WorkerId, int FencingToken);
}
