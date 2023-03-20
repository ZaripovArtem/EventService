using Features;
using Features.Events.Data;
using FluentValidation.AspNetCore;
using IdentityModel.Client;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

builder.Services.AddSingleton<FakeData>();
builder.Services.AddSingleton<Repository>();

builder.Services.AddSingleton<ICorsPolicyService>(serviceProvider =>
    new DefaultCorsPolicyService(serviceProvider.GetService<ILogger<DefaultCorsPolicyService>>())
    {
        AllowAll = true
    });

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential() // use a valid signing cert in production
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, _ =>
    {
    }).AddOAuth2Introspection("introspection", options =>
    {
        //url of your identityserver
        options.Authority = "http://localhost:5000";
        //value of the api resource from identityserver
        options.ClientId = "myapi";
        //value of the api resource secret from identityserver
        options.ClientSecret = "hardtoguess";
        options.DiscoveryPolicy = new DiscoveryPolicy
        {
            //set to true if you require https for your identityserver
            RequireHttps = false
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("LocalPolicy");
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
