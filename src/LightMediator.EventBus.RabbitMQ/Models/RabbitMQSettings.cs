namespace LightMediator.EventBus.RabbitMQ.Models;

public class RabbitMQSettings
{
    /// <summary>
    /// RabbitMQ host URI (e.g., "rabbitmq://localhost")
    /// </summary>
    public string HostUri { get; set; } = "rabbitmq://localhost";

    /// <summary>
    /// Username for RabbitMQ connection
    /// </summary>
    public string Username { get; set; } = "guest";

    /// <summary>
    /// Password for RabbitMQ connection
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// Virtual host to connect to (default "/")
    /// </summary>
    public string VirtualHost { get; set; } = "/";

    /// <summary>
    /// Whether queues and exchanges are durable
    /// </summary>
    public bool Durable { get; set; } = true;

    /// <summary>
    /// Prefetch count for consumers (controls parallelism)
    /// </summary>
    public ushort PrefetchCount { get; set; } = 10;

    /// <summary>
    /// Enable dead-letter queue support
    /// </summary>
    public bool EnableDeadLetter { get; set; } = true;

    /// <summary>
    /// Enable automatic endpoint configuration
    /// </summary>
    public bool ConfigureEndpoints { get; set; } = true;

    /// <summary>
    /// Optionally override default exchange name (MassTransit uses type-based names)
    /// </summary>
    public string? ExchangeName { get; set; }

    /// <summary>
    /// Enable message retry (MassTransit built-in policy)
    /// </summary>
    public bool EnableRetry { get; set; } = true;

    /// <summary>
    /// Number of retry attempts
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// Retry interval in milliseconds
    /// </summary>
    public int RetryIntervalMs { get; set; } = 1000;
}
