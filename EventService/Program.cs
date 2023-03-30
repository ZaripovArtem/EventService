using Features.Events.Data;
using Features.Events.Validators;
using Features.Settings;
using FluentValidation.AspNetCore;
using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Polly;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(o => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
#pragma warning disable CS0618
builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
#pragma warning restore CS0618

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddSingleton<FakeData>();
builder.Services.AddSingleton<Repository>();

builder.Services.AddSingleton<ICorsPolicyService>(serviceProvider =>
    new DefaultCorsPolicyService(serviceProvider.GetService<ILogger<DefaultCorsPolicyService>>())
    {
        AllowAll = true
    });

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

// ReSharper disable once VariableHidesOuterVariable
builder.Services.AddCors(o => o.AddPolicy("LocalPolicy", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var httpRetryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(2));

builder.Services.AddHttpClient()
    .AddPolicyRegistry().Add("retry", httpRetryPolicy);

builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddHostedService<RabbitMqListener>();
builder.Services.AddSingleton<IMessageService, ImageMessageService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalPolicy");
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();


app.Run();