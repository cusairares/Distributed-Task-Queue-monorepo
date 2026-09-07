using DTQ.Application.Interfaces;
using DTQ.Domain.Interfaces;

using TaskStatus = DTQ.Domain.Enums.TaskStatus;

namespace DTQ.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository _repository;
        private readonly Queue<Guid> _taskQueue = new();

        public TaskService(IRepository repository) 
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<TaskResponseDto>> GetTasksAsync(TaskItemStatus? status)
        {
            var tasks = status is null
                ? await _repository.GetAllTasksAsync()
                : await _repository.GetTasksByStatusAsync(status.Value);

            return tasks.Select(TaskResponseDto.FromDomain).ToList();
        }

        public async Task<bool> CreateTaskAsync(CreateTaskRequest taskRequest)
        {
            var task = TaskItem.Create(taskRequest.name, taskRequest.taskType, taskRequest.payload, taskRequest.maxRetries);

            var completed = await _repository.AddTaskAsync(task);

            if (completed)
        {
                _taskQueue.Enqueue(task.Id);
        }

            return completed;
        }
        {
            if(status is null)
            {
                return await _registry.GetAllTasksAsync();
            }
            return await _registry.GetTasksByStatusAsync(status.Value);
        }
    }
}
