using Features.Events.Domain;
using Features.Settings;
using MongoDB.Driver;

namespace Features.Events.Data;

/// <summary>
/// Класс для работы с MongoDb
/// </summary>
public class Repository
{
    /// <summary>
    /// Коллекция мероприятий
    /// </summary>
    public IMongoCollection<Event> Events;
    /// <summary>
    /// Список Id фотографий
    /// </summary>
    public List<Guid> Image;
    /// <summary>
    /// Список Id комнат
    /// </summary>
    public List<Guid> Room;
    /// <summary>
    /// Список пользователей
    /// </summary>
    public List<User> User;

    /// <summary>
    /// Сервис RMQ для мероприятий
    /// </summary>
    public IMessageService MessageService;

    /// <summary>
    /// Сервис RMQ для афиш
    /// </summary>
    public IMessageService ImageMessageService;

    /// <summary>
    /// Сервис RMQ для пространств
    /// </summary>
    public IMessageService SpaceMessageService;

    /// <summary>
    /// Конструктор. В нем происходит обращение к MongoDb, а также добавление начальных
    /// значений для Image и Room
    /// </summary>
    public Repository()
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

        var mongodbUrl = MongoUrl.Create(connectionString);
        var mongodbClient = new MongoClient(mongodbUrl);
        var database = mongodbClient.GetDatabase(mongodbUrl.DatabaseName);
        Events = database.GetCollection<Event>("order");

        Image = new List<Guid>(GetImageList().Result.ToList());

        Room = new List<Guid>(GetRoomList().Result.ToList());

        User = new List<User>
        {
            new()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2"),
                NickName = "Nick"
            }
        };

        IMessageService messageService = new MessageService();
        MessageService = messageService;
        IMessageService imageMessageService = new ImageMessageService();
        ImageMessageService = imageMessageService;
        IMessageService spaceMessageService = new SpaceMessageService();
        SpaceMessageService = spaceMessageService;
    }

    /// <summary>
    /// Task для получения Id комнат
    /// </summary>
    /// <returns></returns>
    public async Task<List<Guid>> GetRoomList()
    {
        const string url = "http://host.docker.internal:7002/room";

        HttpClientHandler handler = new();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        var httpClient = new HttpClient(handler);

        var list = await httpClient.GetFromJsonAsync<List<Guid>>(url);

        return list!;
    }

    /// <summary>
    /// Task для получения Id изображения
    /// </summary>
    /// <returns></returns>
    public async Task<List<Guid>> GetImageList()
    {
        const string url = "http://host.docker.internal:7003/image";

        HttpClientHandler handler = new();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        var httpClient = new HttpClient(handler);

        var list = await httpClient.GetFromJsonAsync<List<Guid>>(url);

        return list!;
    }
}
