using DTQ.Domain;
using DTQ.Domain.Enums;

namespace DTQ.Application.Responses
{
    public record TaskResponseDto(
        Guid Id,
        string Name,
        string TaskType,
        string Payload,
        TaskItemStatus Status,
        int MaxRetries,
        int RetriesCount,
        DateTimeOffset CreationDate)
    {
        public static TaskResponseDto FromDomain(TaskItem task)
        {
            return new TaskResponseDto(
                task.Id,
                task.Name,
                task.TaskType,
                task.Payload,
                task.Status,
                task.MaxRetries,
                task.RetriesCount,
                task.CreationDate
            );
        }
    }
}
