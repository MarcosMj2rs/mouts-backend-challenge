using Ambev.DeveloperEvaluation.Application.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Events
{
    public class LogEventPublisher : IEventPublisher
    {
        private readonly ILogger<LogEventPublisher> _logger;

        public LogEventPublisher(ILogger<LogEventPublisher> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(string eventType, object payload, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("📢 Domain Event: {EventType} {@Payload}", eventType, payload);

            return Task.CompletedTask;
        }
    }
}
