using DTQ.Domain;
using DTQ.Domain.Enums;
using DTQ.Domain.Interfaces;
using System.Collections.Concurrent;

namespace DTQ.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        // Mock will be replaced with PostgreSQL
        private static readonly ConcurrentDictionary<int, TaskItem> _tasks = new();
        private static int _nextId = 0;

        public Task<bool> AddTaskAsync(TaskItem task)
        {
            task.AssignId(Interlocked.Increment(ref _nextId));
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

        public Task<TaskItem?> GetByIdAsync(int taskId)
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

        public Task<TaskItem?> ClaimNextTaskAsync()
        {
            // Needs DB query mock / atomic fetch & update
            // update should fencingToken += 1
            return Task.FromResult<TaskItem?>(null);
        }

        public Task<bool> MarkAsSuccessAsync(int taskId, int fencingToken)
        {
            // TODO: Atomic DB query — validate fencingToken, set Status = Success
            // UPDATE tasks SET status = 'Success'
            // WHERE id = @taskId AND fencing_token = @fencingToken
            return Task.FromResult(false);
        }

        public Task<bool> MarkAsFailedAsync(int taskId, int fencingToken, string? errorMessage)
        {
            // TODO: Atomic DB query — validate fencingToken, set Status = Fail (or increment retry), store error
            // UPDATE tasks SET status = 'Fail', error_message = @errorMessage
            // WHERE id = @taskId AND fencing_token = @fencingToken
            return Task.FromResult(false);
        }

        public Task<bool> HeartbeatAsync(int taskId, int fencingToken)
        {
            // TODO: Atomic DB query — validate fencingToken is still current
            // SELECT 1 FROM tasks WHERE id = @taskId AND fencing_token = @fencingToken AND status = 'Processing'
            return Task.FromResult(false);
        }
    }
}
