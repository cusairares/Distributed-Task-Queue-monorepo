using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTQ.Domain.Interfaces
{
    public interface ITask
    {
        Guid Id { get; }
        int MaxRetries { get; }
        int RetriesCount { get; }
        int FencingToken { get; }

        TaskStatus Status { get; }
        string LeaseExpiresAt { get; }

        //placeholder until JSON
        string Payload { get; }
    }
}
