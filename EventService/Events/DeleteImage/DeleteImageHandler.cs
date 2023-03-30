using Features.Events.Data;
using MediatR;

namespace Features.Events.DeleteImage
{
    /// <summary>
    /// Реализация обработчика DeleteImageCommand
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
    public class DeleteImageHandler : IRequestHandler<DeleteImageCommand>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public DeleteImageHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Удаление афиши
        /// </summary>
        public Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            _data.ImageMessageService.Enqueue($"Удаление афиши {request.Id}");
            return Task.CompletedTask;
        }
    }
}
