//using Tasks.Application.Abstractions.Messaging;

//namespace Tasks.Infrastructure.EventBus;

//public sealed class EventBus : IEventBus
//{
//    //todo: MassTransit
//    private readonly IBus _bus;

//    public EventBus(IBus bus) => _bus = bus;

//    public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent,
//        CancellationToken cancellationToken = default)
//        where TIntegrationEvent : IIntegrationEvent =>
//        await _bus.PublishAsync(integrationEvent, cancellationToken);
//}