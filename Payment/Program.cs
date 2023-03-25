var builder = WebApplication.CreateBuilder(args);

var paymentOperations = new List<PaymentOperation>();

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

app.MapGet("/payment", () => paymentOperations);

app.MapGet("/payment/{id}", (int id) =>
{

});

app.Run();

public class PaymentOperation
{
    public Guid Id { get; set; }
    public PaymentState State { get; set; }
    public DateTime? DateCreation { get; set; }
    public DateTime? DateConfirmation { get; set; }
    public DateTime? DateCancellation { get; set;}
}

public enum PaymentState
{
    hold,
    confirmed,
    canceled
}