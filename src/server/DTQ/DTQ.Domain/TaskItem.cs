using DTQ.Domain.Enums;
using System.Security.Claims;

namespace DTQ.Domain
{
    public class TaskItem
    {
        #region Properties
        private Guid _id;
        private string _name;
        private string _taskType;
        private TaskItemStatus _status;
        private string _payload;

        private int _maxRetries;
        private int _retriesCount;
        private int? _fencingToken;


        private DateTimeOffset? _leaseExpiresAt;
        private DateTimeOffset _creationDate;

        private Guid? _workerId;
        public Guid Id => _id;

        private Guid? WorkderId => _workerId;
        public string Name => _name;

        public string TaskType => _taskType;

        public int MaxRetries => _maxRetries;

        public int RetriesCount => _retriesCount;

        public int? FencingToken => _fencingToken;

        public TaskItemStatus Status => _status;

        public DateTimeOffset? LeaseExpiresAt => _leaseExpiresAt;

        public string Payload => _payload;

        public DateTimeOffset CreationDate => _creationDate;

        #endregion
        
        public TaskItem(Guid id, string name, string taskType, string payload, int maxRetries, DateTimeOffset creationDate)
        {
            _id = id;
            _name = name;
            _taskType = taskType;
            _status = TaskItemStatus.Pending;
            _payload = payload;
            _maxRetries = maxRetries;
            _retriesCount = 0;
            _fencingToken = null;
            _leaseExpiresAt = null;
            _creationDate = creationDate;
            _workerId = null;
        }

        public static TaskItem Create(string name, string taskType, string payload, int maxRetries)
        {
            return new TaskItem(
                Guid.NewGuid(),
                name,
                taskType,
                payload,
                maxRetries,
                DateTimeOffset.UtcNow
            );
        }
    }
}
