using Features.Events.Data;
using MediatR;

namespace Features.Events.GiveTicketToUser;

/// <summary>
/// Реализация обработчика GiveTicketToUserCommand
/// </summary>
public class GiveTicketToUserHandler : IRequestHandler<GiveTicketToUserCommand>
{
    private readonly Repository _data;

    /// <summary>
    /// Конструктор, для получения данных с MongoDb
    /// </summary>
    public GiveTicketToUserHandler(Repository data)
    {
        _data = data;
    }

    /// <summary>
    /// Реализация обработчика
    /// </summary>
    public async Task Handle(GiveTicketToUserCommand request, CancellationToken cancellationToken)
    {
        await _data.GiveTicketToUser(request.EventId, request.TicketId, request.UserId);
    }
}
