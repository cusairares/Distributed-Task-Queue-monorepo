using DTQ.Application.Interfaces;
using TaskStatus = DTQ.Domain.Enums.TaskStatus;
namespace DTQ.ApiGateway.Endpoints
{
    public static class ClientEndpoints
    {
        public static RouteGroupBuilder MapServiceEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("dtq/api/v1");

            group.MapGet("/tasks",async  (TaskStatus? status, ITaskService service) =>
            {
                var tasks = await service.GetTasksAsync(status);

                return Results.Ok(tasks);
            });

            return group;
        }
    }
}
