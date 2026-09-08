using DTQ.Domain;
using DTQ.Domain.Enums;
using DTQ.Domain.Interfaces;
using System.Collections.Concurrent;
using System.Net.NetworkInformation;

namespace DTQ.Infrastructure.Repositories
{
    public class TaskRepository : IRepository
    {

        //mock will be replaced with PostgreSQL
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

        public bool UpdateByIdAsync(TaskItem item)
        {
            return _tasks.TryUpdate(item.Id,item, _tasks[item.Id]);
        }

        public Task<TaskItem> ClaimNextTaskAsync(Guid workerId)
        {
            //needs DB query mock
            //using LOCK + fetching and also updating the task 
            //if (Status != TaskItemStatus.Pending) return false;

            //if (Status == TaskItemStatus.Processing && RetriesCount >= MaxRetries) return false;

            //_fencingToken = 0;
            //_leaseExpiresAt = DateTimeOffset.UtcNow.AddMilliseconds(1000);
            //_retriesCount++;
            //_status = TaskItemStatus.Processing;
            //_workerId = workerId;

            return null;
        }
    }
}
