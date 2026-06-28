using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync(string eventType, object payload, CancellationToken cancellationToken = default);
    }
}
