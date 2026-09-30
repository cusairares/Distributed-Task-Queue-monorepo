using DTQ.Domain;
using DTQ.Domain.Enums;
using DTQ.Domain.Interfaces;
using System.Collections.Concurrent;

namespace DTQ.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        // Mock will be replaced with PostgreSQL
        private static readonly ConcurrentDictionary<Guid, TaskItem> _tasks = new();

        public Task<bool> AddTaskAsync(TaskItem task)
        {
            var added = _tasks.TryAdd(task.Id, task);
            return Task.FromResult(added);
        }

        public Task<IReadOnlyList<TaskItem>> GetAllTasksAsync()
        {
            IReadOnlyList<TaskItem> list = _tasks.Values.ToList();
            return Task.FromResult(list);
        }

        public Task<IReadOnlyList<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status)
        {
            IReadOnlyList<TaskItem> list = _tasks.Values.Where(t => t.Status == status).ToList();
            return Task.FromResult(list);
        }

        public Task<TaskItem?> GetByIdAsync(Guid taskId)
        {
            _tasks.TryGetValue(taskId, out var task);
            return Task.FromResult(task);
        }

        public Task<bool> UpdateByIdAsync(TaskItem item)
        {
            if (!_tasks.TryGetValue(item.Id, out var existing))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(_tasks.TryUpdate(item.Id, item, existing));
        }

        public Task<TaskItem?> ClaimNextTaskAsync(Guid workerId)
        {
            // Needs DB query mock / atomic fetch & update
            return Task.FromResult<TaskItem?>(null);
        }

        public Task<bool> MarkAsSuccessAsync(Guid id)
        {
            // Mock WIP - fetch and update with lock of task
            return Task.FromResult(false);
        }
    }
}
