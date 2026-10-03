using DTQ.ApiGateway.Endpoints;
using DTQ.Application.Interfaces;
using DTQ.Application.Services;
using DTQ.Domain.Interfaces;
using DTQ.Infrastructure;
using DTQ.Infrastructure.Data;
using DTQ.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddToInfrastructure(builder.Configuration.GetConnectionString("Default"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Note to self: Swagger try execute does not properly populate payload: json.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapClientEndpoints();
app.MapWorkerEndpoints();

app.Run();