using DTQ.Domain;
using DTQ.Domain.Enums;
using DTQ.Domain.Interfaces;
using DTQ.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DTQ.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context; 
        }

        // Mock will be replaced with PostgreSQL
        private static readonly ConcurrentDictionary<int, TaskItem> _tasks = new();
        private static int _nextId = 0;

        public async Task<bool> AddTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IReadOnlyList<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<IReadOnlyList<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status)
        {
            return await _context.Tasks
            .AsNoTracking()
            .Where(x => x.Status == status)
            .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int taskId)
        {
            return await _context.Tasks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == taskId);
        }

        public async Task<bool> UpdateByIdAsync(TaskItem item)
        {
            int rowsAffected = await _context.Tasks
                    .Where(x => x.Id == item.Id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(x => x.Status, item.Status)
                        .SetProperty(x => x.Payload, item.Payload)
                        .SetProperty(x => x.MaxRetries, item.MaxRetries)
                        .SetProperty(x => x.RetriesCount, item.RetriesCount)
                        .SetProperty(x => x.FencingToken, item.FencingToken)
                        .SetProperty(x => x.ErrorMessage, item.ErrorMessage));

            return rowsAffected > 0;
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
