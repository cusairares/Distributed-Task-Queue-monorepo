// DTQ.ApiGateway/Program.cs
using DTQ.ApiGateway.Endpoints;
using DTQ.Application.Interfaces;
using DTQ.Application.Services;
using DTQ.Domain.Interfaces;
// using DTQ.Infrastructure.Persistence; // Wherever your IRegistry implementation lives

var builder = WebApplication.CreateBuilder(args);

// 1. Register Services in DI container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your application service and registry implementation:
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// 2. Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 3. Map your endpoints
app.MapServiceEndpoints();

app.Run();