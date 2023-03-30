using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Features.Events.Domain;

/// <summary>
/// Класс событий
/// </summary>
[Serializable, BsonIgnoreExtraElements]
public class Event
{
    /// <summary>
    /// Id события
    /// </summary>
    [BsonId, BsonElement("_id")]
    public Guid Id { get; set; }
    /// <summary>
    /// Начало мероприятия
    /// </summary>
    [BsonElement("start_event"), BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset StartEvent { get; set; }
    /// <summary>
    /// Окончание мероприятия
    /// </summary>
    [BsonElement("end_event"), BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset EndEvent { get; set; }
    /// <summary>
    /// Название мероприятия
    /// </summary>
    [BsonElement("event_name")]
    public string? EventName { get; set; }
    /// <summary>
    /// Id фотографии афиши (при наличии)
    /// </summary>
    [BsonElement("photo_id")]
    public Guid? PhotoId { get; set; }
    /// <summary>
    /// Id пространства 
    /// </summary>
    [BsonElement("place_id")]
    public Guid PlaceId { get; set; }
    /// <summary>
    /// Список билетов
    /// </summary>
    [BsonElement("ticket")]
    public List<Ticket>? Ticket { get; set; }
    /// <summary>
    /// Предусмотрены ли места в билетах
    /// </summary>
    [BsonElement("has_place")]
    public bool HasPlace { get; set; }
    /// <summary>
    /// Цена билета.
    /// Если вход свободный - 0
    /// </summary>
    public decimal Price { get; set; }
}
