using DTQ.Domain.Interfaces;
using TaskStatus = DTQ.Domain.Enums.TaskStatus;


namespace DTQ.Application.Interfaces
{
    public interface ITaskService
    {
        public Task<IReadOnlyList<ITask>> GetTasksAsync(TaskStatus? status);
    }
}
