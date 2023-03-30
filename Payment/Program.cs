using Payment;

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

app.MapPost("/payment/create", (PaymentOperation paymentOperation) =>
{
    app.Logger.LogInformation("Http POST запрос на создание платежной операции");
    operations.Add(paymentOperation);
    return paymentOperation;
});

app.MapPut("/payment/confirm/{id}", (Guid id) =>
{
    var paymentOperation = operations.FirstOrDefault(p => p.Id == id);

    if (paymentOperation == null) return Results.NotFound(new { message = "Платежная операция не найдена" });

    paymentOperation.DateConfirmation = DateTimeOffset.Now;
    paymentOperation.State = PaymentState.Confirmed;
    app.Logger.LogInformation("Http PUT запрос на подтверждение платежной операции");
    return Results.Json(paymentOperation);
});

app.MapPut("/payment/cancel/{id}", (Guid id) =>
{
    var paymentOperation = operations.FirstOrDefault(p => p.Id == id);

    if (paymentOperation == null) return Results.NotFound(new { message = "Платежная операция не найдена" });

    paymentOperation.DateCancellation = DateTimeOffset.Now;
    paymentOperation.State = PaymentState.Cancelled;
    app.Logger.LogInformation("Http PUT запрос на отмену платежной операции");
    return Results.Json(paymentOperation);
});

app.UseHttpLogging();

app.Run();