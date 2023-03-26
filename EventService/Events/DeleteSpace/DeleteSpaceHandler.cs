using Features.Events.Data;
using MediatR;

namespace Features.Events.DeleteSpace;

/// <summary>
/// Реализация обработчика DeleteSpaceCommand
/// </summary>
public class DeleteSpaceHandler : IRequestHandler<DeleteSpaceCommand>
{
    private readonly Repository _data;

    /// <summary>
    /// Конструктор, для получения данных с MongoDb
    /// </summary>
    public DeleteSpaceHandler(Repository data)
    {
        _data = data;
    }

    /// <summary>
    /// Реализация обработчика
    /// </summary>
    public async Task Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
    {
        await _data.DeleteSpace(request.Id);
    }
}