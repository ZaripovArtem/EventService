var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var operations = new List<PaymentOperation>();

app.MapGet("/payment", () => operations);

app.MapGet("/payment/create", () =>
{
    PaymentOperation paymentOperation = new();
    paymentOperation.Id = Guid.NewGuid();
    paymentOperation.DateCreation = DateTime.Now;
    paymentOperation.State = PaymentState.Hold;
    operations.Add(paymentOperation);
    return paymentOperation;
});

app.MapPut("/payment/confirm/{id}", (Guid id) =>
{
    var paymentOperation = operations.FirstOrDefault(p => p.Id == id);

    if (paymentOperation == null) return Results.NotFound(new { message = "Платежная операция не найдена" });

    paymentOperation.DateConfirmation = DateTime.Now;
    paymentOperation.State = PaymentState.Confirmed;
    app.Logger.LogInformation("Http PUT запрос на подтверждение платежной операции");
    return Results.Json(paymentOperation);
});

app.MapPut("/payment/cancel/{id}", (Guid id) =>
{
    var paymentOperation = operations.FirstOrDefault(p => p.Id == id);

    if (paymentOperation == null) return Results.NotFound(new { message = "Платежная операция не найдена" });

    paymentOperation.DateCancellation = DateTime.Now;
    paymentOperation.State = PaymentState.Cancelled;
    app.Logger.LogInformation("Http PUT запрос на отмену платежной операции");
    return Results.Json(paymentOperation);
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
    Hold,
    Confirmed,
    Cancelled
}