using DTQ.Application.Interfaces;
using DTQ.Domain.Interfaces;

using TaskStatus = DTQ.Domain.Enums.TaskStatus;

namespace DTQ.Application.Services
{
    public class TaskService : ITaskService
    {
        private IRegistry _registry;

        public TaskService(IRegistry registry) 
        {
            this._registry = registry;
        }
        public async Task<IReadOnlyList<ITask>> GetTasksAsync(TaskStatus? status)
        {
            if(status is null)
            {
                return await _registry.GetAllTasksAsync();
            }
            return await _registry.GetTasksByStatusAsync(status.Value);
        }
    }
}
