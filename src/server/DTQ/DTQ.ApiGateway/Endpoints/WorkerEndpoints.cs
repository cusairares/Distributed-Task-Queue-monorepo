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

            group.MapGet("/claim", async ([FromBody] ClaimTaskRequest request, ITaskService service) => 
            {
                var task = await service.ClaimTaskAsync(request.workerId);

                return task is not null ? Results.Ok(task) : Results.NoContent();

            }).WithSummary("Get a pending task from queue");

            return group;
        }
    }
}
