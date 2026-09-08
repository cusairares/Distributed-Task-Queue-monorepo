using DTQ.Domain.Enums;

namespace DTQ.Domain.Interfaces
{
    public interface IRepository
    {
        Task<IReadOnlyList<TaskItem>> GetAllTasksAsync();

        Task<IReadOnlyList<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status);

        Task<bool> AddTaskAsync(TaskItem task);

        Task<TaskItem?> GetByIdAsync(Guid taskId);

        Task<TaskItem?> ClaimNextTaskAsync(Guid workerId);

        bool UpdateByIdAsync(TaskItem item);
    }
}
