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
            // update should fencingToken += 1
            return Task.FromResult<TaskItem?>(null);
        }

        public Task<bool> MarkAsSuccessAsync(Guid taskId, Guid workerId, int fencingToken)
        {
            // TODO: Atomic DB query — validate fencingToken + workerId, set Status = Completed, clear lease
            // UPDATE tasks SET status = 'Completed', lease_expires_at = NULL
            // WHERE id = @taskId AND worker_id = @workerId AND fencing_token = @fencingToken
            return Task.FromResult(false);
        }

        public Task<bool> MarkAsFailedAsync(Guid taskId, Guid workerId, int fencingToken)
        {
            // TODO: Atomic DB query — validate fencingToken + workerId, set Status = Failed (or increment retry), clear lease
            // UPDATE tasks SET status = 'Failed', lease_expires_at = NULL
            // WHERE id = @taskId AND worker_id = @workerId AND fencing_token = @fencingToken
            return Task.FromResult(false);
        }

        public Task<bool> HeartbeatAsync(Guid taskId, Guid workerId, int fencingToken)
        {
            // TODO: Atomic DB query — validate fencingToken + workerId, extend lease
            // UPDATE tasks SET lease_expires_at = NOW() + INTERVAL '30s'
            // WHERE id = @taskId AND worker_id = @workerId AND fencing_token = @fencingToken
            return Task.FromResult(false);
        }
    }
}
