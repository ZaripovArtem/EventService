var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var imageList = new List<Guid>()
{
    new("332bbf56-883f-41c0-bbac-6a49fda5c361"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c362"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c363"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c364"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c365"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c366"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c367"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c368"),
    new("332bbf56-883f-41c0-bbac-6a49fda5c369")
};

app.MapGet("/image", () =>
{
    app.Logger.LogInformation("Http запрос на получение списка изображения для афиши");
    return imageList;
});

app.Run();
