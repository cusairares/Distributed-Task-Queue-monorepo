using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTQ.Application.Requests
{
    public record CreateTaskRequest(string name, string taskType, string payload, int maxRetries);
}
