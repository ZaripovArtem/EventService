using Room;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RabbitMqListener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var roomList = new List<Guid>()
{
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b1"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b2"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b3"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b4"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b5"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b6"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b7"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b8"),
    new("9485dd5d-3dfd-4fbb-9f1c-2746016d92b9")
};

app.MapGet("/room", () =>
{
    app.Logger.LogInformation("Http запрос на получение списка пространств");
    return roomList;
});

app.UseHttpLogging();

app.Run();

