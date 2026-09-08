using DTQ.Application.Requests;
using DTQ.Application.Responses;
using DTQ.Domain.Enums;

namespace DTQ.Application.Interfaces
{
    public interface ITaskService
    {
        Task<bool> CreateTaskAsync(CreateTaskRequest taskRequest);
        Task<IReadOnlyList<TaskResponseDto>> GetTasksAsync(TaskItemStatus? status);
        Task<TaskResponseDto?> ClaimTaskAsync(Guid workerId);
    }
}
