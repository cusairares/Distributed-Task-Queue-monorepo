using DTQ.Application.Interfaces;
using DTQ.Application.Requests;
using DTQ.Application.Responses;
using DTQ.Domain.Enums;

namespace DTQ.ApiGateway.Endpoints
{
    public static class ClientEndpoints
    {

        public static RouteGroupBuilder MapClientEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("dtq/api/v1").WithTags("Client Endpoints");

            group.MapGet("/tasks",async  (TaskItemStatus? status, ITaskService service) =>
            {
                var tasks = await service.GetTasksAsync(status);

                return Results.Ok(tasks);
            }).WithSummary("Get tasks with optional status filtering");

            group.MapPost("/tasks", async (CreateTaskRequest request, ITaskService service) =>
            {
                if (string.IsNullOrWhiteSpace(request.name))
                {
                    return Results.BadRequest(new { error = "Task name is required." });
                }

                if (string.IsNullOrWhiteSpace(request.taskType))
                {
                    return Results.BadRequest(new { error = "Task type is required." });
                }

                if (request.maxRetries < 0)
                {
                    return Results.BadRequest(new { error = "MaxRetries cannot be negative." });
                }

                var completed = await service.CreateTaskAsync(request);
                if (!completed)
                {
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);
                }

                return Results.Ok();
            }).WithSummary("Create task");
            
            return group;
        }
    }
}
