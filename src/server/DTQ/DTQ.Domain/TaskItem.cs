using DTQ.Domain.Enums;

namespace DTQ.Domain
{
    public class TaskItem
    {
        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string TaskType { get; private set; } = string.Empty;

        public TaskItemStatus Status { get; private set; }

        public string? Payload { get; private set; }

        public int MaxRetries { get; private set; }

        public int RetriesCount { get; private set; }

        public int FencingToken { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public string? ErrorMessage { get; private set; }

        #endregion

        // Required by EF Core
        public TaskItem()
        {
        }

        //Note for self: constructor is not used by the EF. It can populate the entites through the private setters
        public TaskItem(string name, string taskType, string? payload, int maxRetries)
        {
            Name = name;
            TaskType = taskType;
            Status = TaskItemStatus.Pending;
            Payload = payload;
            MaxRetries = maxRetries;
            // Id, RetriesCount, FencingToken and CreatedAt are provided by database defaults.
        }

        public static TaskItem Create(string name, string taskType, string? payload, int maxRetries)
        {
            return new TaskItem(name, taskType, payload, maxRetries);
        }

        /// <summary>
        /// Applies the values a database would generate on insert (used by non-database repositories).
        /// </summary>
        public void ApplyInsertDefaults(int id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }
    }
}
