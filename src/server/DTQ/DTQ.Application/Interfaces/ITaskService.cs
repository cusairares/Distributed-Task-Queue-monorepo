using DTQ.Domain.Interfaces;
using TaskStatus = DTQ.Domain.Enums.TaskStatus;


namespace DTQ.Application.Interfaces
{
    public interface ITaskService
    {
        Task<bool> CreateTaskAsync(CreateTaskRequest taskRequest);
        Task<IReadOnlyList<TaskResponseDto>> GetTasksAsync(TaskItemStatus? status);
    }
}
