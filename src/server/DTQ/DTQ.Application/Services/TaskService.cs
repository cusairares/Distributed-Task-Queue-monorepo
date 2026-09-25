using DTQ.Application.Interfaces;
using DTQ.Application.Requests;
using DTQ.Application.Responses;
using DTQ.Domain;
using DTQ.Domain.Enums;
using DTQ.Domain.Interfaces;

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
            var task = TaskItem.Create(taskRequest.Name, taskRequest.TaskType, taskRequest.Payload, taskRequest.MaxRetries);

            var completed = await _repository.AddTaskAsync(task);

            if (completed)
            {
                _taskQueue.Enqueue(task.Id);
            }

            return completed;
        }

        public async Task<TaskResponseDto?> ClaimTaskAsync(Guid workerId)
        {

            var task = await _repository.ClaimNextTaskAsync(workerId);

            if(task == null)
            {
                return null;
            }
            
            return TaskResponseDto.FromDomain(task);
        }
    }
}
