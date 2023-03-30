using Features.Events.Data;
using MediatR;

namespace Features.Events.DeleteSpace;

/// <summary>
/// Реализация обработчика DeleteSpaceCommand
/// </summary>
// ReSharper disable once UnusedMember.Global Обработчик
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
    /// Удаление пространства
    /// </summary>
    public Task Handle(DeleteSpaceCommand request, CancellationToken cancellationToken)
    {
        _data.SpaceMessageService.Enqueue($"Удаление пространства {request.Id}");
        return Task.CompletedTask;
    }
}