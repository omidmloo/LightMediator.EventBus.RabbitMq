using static System.Collections.Specialized.BitVector32;

namespace LightMediator.EventBus.RabbitMQ;

public static class HostingExtensions
{
    public static LightMediatorEventBusOptions UseRabbitMQ(
      this LightMediatorEventBusOptions serviceBusOptions,
      IConfiguration configuration,
      string rabbitMqSectionName)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        if (string.IsNullOrWhiteSpace(rabbitMqSectionName)) throw new ArgumentException("RabbitMq section name is required.");

        return UseRabbitMQ(serviceBusOptions, configuration.GetSection(rabbitMqSectionName));
    }

   public static LightMediatorEventBusOptions UseRabbitMQ(
        this LightMediatorEventBusOptions serviceBusOptions,
    IConfigurationSection configurationSection)
    {
        if (configurationSection == null) throw new ArgumentNullException(nameof(configurationSection));

        RabbitMQSettings? settings = configurationSection.Get<RabbitMQSettings>();
        if (settings == null)
            throw new RabbitMQConfigurationException("RabbitMQ settings are invalid or missing.");

        return UseRabbitMQ(serviceBusOptions, settings);
    }

    public static LightMediatorEventBusOptions UseRabbitMQ(
        this LightMediatorEventBusOptions serviceBusOptions, 
        RabbitMQSettings settings)
    {
        if (settings == null)
            throw new RabbitMQConfigurationException("RabbitMQ settings are invalid or missing.");

        serviceBusOptions.ServiceCollection.AddSingleton<ILightMediatorEventBus, RabbitMQEventBus<RabbitMQEvent>>();
        serviceBusOptions.ServiceCollection.AddMassTransit(cfg =>
        {
            cfg.AddConsumer<RabbitMQEventBus<RabbitMQEvent>>();

            cfg.UsingRabbitMq((context, cfgRabbit) =>
            {
                cfgRabbit.Host(settings.HostUri, h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
                cfgRabbit.ReceiveEndpoint("order-created-queue", e =>
                {
                    e.ConfigureConsumer<RabbitMQEventBus<RabbitMQEvent>>(context);
                });
            });
        });

        // Register your publisher
        serviceBusOptions.ServiceCollection.AddScoped<IRabbitMqEventPublisher, RabbitMqEventPublisher>();
        return serviceBusOptions;
    }
}
