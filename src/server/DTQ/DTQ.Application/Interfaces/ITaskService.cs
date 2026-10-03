using DTQ.Application.Common;
using DTQ.Application.Requests;
using DTQ.Application.Responses;
using DTQ.Domain.Enums;

namespace DTQ.Application.Interfaces
{
    public interface ITaskService
    {
        Task<Result<TaskResponseDto>> CreateTaskAsync(CreateTaskRequest taskRequest);
        Task<IReadOnlyList<TaskResponseDto>> GetTasksAsync(TaskItemStatus? status);
        Task<Result<TaskResponseDto>> ClaimTaskAsync();
        Task<Result> MarkAsSuccessAsync(int taskId, UpdateTaskRequest request);
        Task<Result> MarkAsFailedAsync(int taskId, UpdateTaskRequest request);
        Task<Result> HeartbeatAsync(int taskId, UpdateTaskRequest request);
    }
}
