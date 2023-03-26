using Features.Events.Data;
using MediatR;

namespace Features.Events.DeleteImage
{
    /// <summary>
    /// Реализация обработчика DeleteImageCommand
    /// </summary>
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
        /// Реализация обработчика
        /// </summary>
        public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            await _data.DeleteImage(request.Id);
        }
    }
}
