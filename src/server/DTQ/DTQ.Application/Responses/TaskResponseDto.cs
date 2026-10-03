using DTQ.Domain;
using DTQ.Domain.Enums;

namespace DTQ.Application.Responses
{
    public record TaskResponseDto(
        int Id,
        string? Payload,
        TaskItemStatus Status,
        int MaxRetries,
        int RetriesCount,
        DateTime CreatedAt,
        int FencingToken,
        string? ErrorMessage)
    {
        public static TaskResponseDto FromDomain(TaskItem task)
        {
            return new TaskResponseDto(
                task.Id,
                task.Payload,
                task.Status,
                task.MaxRetries,
                task.RetriesCount,
                task.CreatedAt,
                task.FencingToken,
                task.ErrorMessage
            );
        }
    }
}
