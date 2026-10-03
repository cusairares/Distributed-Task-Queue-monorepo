using DTQ.Application.Interfaces;
using DTQ.Application.Requests;
using DTQ.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DTQ.ApiGateway.Endpoints
{
    public static class WorkerEndpoints
    {
        public static RouteGroupBuilder MapWorkerEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("dtq/api/v1").WithTags("Worker Endpoints");

            group.MapPost("/claim", async (ITaskService service) => 
            {
                var result = await service.ClaimTaskAsync();

                return result.IsSuccess ? Results.Ok(result.Value) : Results.NoContent();

            }).WithSummary("Claim a pending task from queue").WithDescription("We query " +
            "and update (Status and Fencing Token the db in one atomic action");

            group.MapPost("/tasks/{taskId:int}/complete", async (int taskId, [FromBody] UpdateTaskRequest request, ITaskService service) =>
            {
                var result = await service.MarkAsSuccessAsync(taskId, request);

                return result.IsSuccess 
                    ? Results.Ok() 
                    : Results.Problem(detail: result.Error, statusCode: StatusCodes.Status400BadRequest);
            }).WithSummary("Mark a task as completed");

            group.MapPost("/tasks/{taskId:int}/fail", async (int taskId, [FromBody] UpdateTaskRequest request, ITaskService service) =>
            {
                var result = await service.MarkAsFailedAsync(taskId, request);

                return result.IsSuccess 
                    ? Results.Ok() 
                    : Results.Problem(detail: result.Error, statusCode: StatusCodes.Status400BadRequest);
            }).WithSummary("Mark a task as failed");

            group.MapPost("/tasks/{taskId:int}/heartbeat", async (int taskId, [FromBody] UpdateTaskRequest request, ITaskService service) =>
            {
                var result = await service.HeartbeatAsync(taskId, request);

                return result.IsSuccess 
                    ? Results.Ok() 
                    : Results.Problem(detail: result.Error, statusCode: StatusCodes.Status400BadRequest);
            }).WithSummary("Extend lease on a claimed task");

            return group;
        }
    }
}
