using EsAppTest.Api.Endpoints.Payments.Mapper;
using EsAppTest.Core.Services.Interfaces.Payments.Services;
using EsAppTest.Infrastructure;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Infra (DbContext + Repository)
builder.Services.AddInfrastructure(builder.Configuration);

// FastEndpoints + Swagger
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

// AutoMapper (registra tu profile)
builder.Services.AddAutoMapper(typeof(PaymentsProfile).Assembly);

// Core service
builder.Services.AddScoped<PaymentsService>();

var app = builder.Build();

app.UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api");
app.UseSwaggerGen();

app.Run();
