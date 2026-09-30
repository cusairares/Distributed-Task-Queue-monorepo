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
        Task<Result<TaskResponseDto>> ClaimTaskAsync(Guid workerId);
        Task<Result> MarkAsSuccessAsync(Guid id);
    }
}
