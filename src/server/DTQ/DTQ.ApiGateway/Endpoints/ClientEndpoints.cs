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
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Results.Problem(detail: "Task name is required.", statusCode: StatusCodes.Status400BadRequest, title: "Validation Error");
                }

                if (string.IsNullOrWhiteSpace(request.TaskType))
                {
                    return Results.Problem(detail: "Task type is required.", statusCode: StatusCodes.Status400BadRequest, title: "Validation Error");
                }

                if (request.MaxRetries < 0)
                {
                    return Results.Problem(detail: "MaxRetries cannot be negative.", statusCode: StatusCodes.Status400BadRequest, title: "Validation Error");
                }

                var result = await service.CreateTaskAsync(request);
                if (result.IsFailure)
                {
                    return Results.Problem(detail: result.Error, statusCode: StatusCodes.Status500InternalServerError);
                }

                return Results.Created($"/dtq/api/v1/tasks/{result.Value.Id}", result.Value);
            }).WithSummary("Create task");
            
            return group;
        }
    }
}
