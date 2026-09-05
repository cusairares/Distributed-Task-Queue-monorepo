using TaskStatus = DTQ.Domain.Enums.TaskStatus;

namespace DTQ.Domain.Interfaces
{
    public interface IRegistry
    {
        Task<IReadOnlyList<ITask>> GetAllTasksAsync();

        Task<IReadOnlyList<ITask>> GetTasksByStatusAsync(TaskStatus status);
    }
}
