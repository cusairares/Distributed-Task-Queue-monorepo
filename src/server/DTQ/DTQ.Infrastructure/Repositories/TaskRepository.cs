using System.Collections.Concurrent;
using DTQ.Domain;
using DTQ.Domain.Enums;
using DTQ.Domain.Interfaces;

namespace DTQ.Infrastructure.Repositories
{
    public class TaskRepository : IRepository
    {
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
    }
}
