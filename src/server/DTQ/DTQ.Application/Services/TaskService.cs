using DTQ.Application.Common;
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
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository) 
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

        public async Task<Result<TaskResponseDto>> CreateTaskAsync(CreateTaskRequest taskRequest)
        {
            var task = TaskItem.Create(taskRequest.Name, taskRequest.TaskType, taskRequest.Payload, taskRequest.MaxRetries);

            var completed = await _repository.AddTaskAsync(task);

            if (!completed)
            {
                return Result<TaskResponseDto>.Failure("Failed to add task to repository.");
            }

            return Result<TaskResponseDto>.Success(TaskResponseDto.FromDomain(task));
        }

        public async Task<Result<TaskResponseDto>> ClaimTaskAsync()
        {
            var task = await _repository.ClaimNextTaskAsync();

            if (task == null)
            {
                return Result<TaskResponseDto>.Failure("No pending tasks available to claim.");
            }
            
            return Result<TaskResponseDto>.Success(TaskResponseDto.FromDomain(task));
        }

        public async Task<Result> MarkAsSuccessAsync(int taskId, UpdateTaskRequest request)
        {
            var completed = await _repository.MarkAsSuccessAsync(taskId, request.FencingToken);

            if (!completed)
            {
                return Result.Failure($"Task with id '{taskId}' could not be marked as success.");
            }

            return Result.Success();
        }

        public async Task<Result> MarkAsFailedAsync(int taskId, UpdateTaskRequest request)
        {
            var errorMessage = request.ErrorMessage;
            if (errorMessage is { Length: > 30 })
            {
                errorMessage = errorMessage[..30];
            }

            var completed = await _repository.MarkAsFailedAsync(taskId, request.FencingToken, errorMessage);

            if (!completed)
            {
                return Result.Failure($"Task with id '{taskId}' could not be marked as failed.");
            }

            return Result.Success();
        }

        public async Task<Result> HeartbeatAsync(int taskId, UpdateTaskRequest request)
        {
            var completed = await _repository.HeartbeatAsync(taskId, request.FencingToken);

            if (!completed)
            {
                return Result.Failure($"Task with id '{taskId}' heartbeat failed. Worker may no longer own this task.");
            }

            return Result.Success();
        }
    }
}
