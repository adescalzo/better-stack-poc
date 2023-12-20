using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add support to logging with Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Logger.LogInformation("The app is starting ...");

//Add support to logging request with Serilog
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//Endpoints
app.MapGet("/", (ILogger<Program> logger) => 
{
    var sm = new SimpleMessage(Guid.NewGuid(), "Hello API Example!!", DateTime.UtcNow);
    logger.LogTrace("Log trace {id} | {message} | {now}", sm.Id, sm.Message, sm.Now);
    return Results.Ok(sm);
})
.WithName("GetRoot")
.WithOpenApi();

app.MapGet("/debug", (ILogger<Program> logger) => 
{
    var sm = new SimpleMessage(Guid.NewGuid(), "Debug Example!!", DateTime.UtcNow);
    logger.LogDebug("Log debug {id} | {message} | {now}", sm.Id, sm.Message, sm.Now);
    return Results.Ok(sm);
})
.WithName("GetDebugExample")
.WithOpenApi();

app.MapGet("/info", (ILogger<Program> logger) => 
{
    var sm = new SimpleMessage(Guid.NewGuid(), "Information Example!!", DateTime.UtcNow);
    logger.LogInformation("Log information {id} | {message} | {now}", sm.Id, sm.Message, sm.Now);
    return Results.Ok(sm);
})
.WithName("GetInformationExample")
.WithOpenApi();

app.MapGet("/warning", (ILogger<Program> logger) => 
{
    var sm = new SimpleMessage(Guid.NewGuid(), "Warning Example!!", DateTime.UtcNow);
    logger.LogWarning("Log warning {id} | {message} | {now}", sm.Id, sm.Message, sm.Now);
    return Results.BadRequest(sm);
})
.WithName("GetWarningExample")
.WithOpenApi();

app.MapGet("/error", (ILogger<Program> logger) => 
{
    var sm = new SimpleMessage(Guid.NewGuid(), "Error Example!!", DateTime.UtcNow);
    logger.LogError("Log error {id} | {message} | {now}", sm.Id, sm.Message, sm.Now);
    return Results.BadRequest(sm);
})
.WithName("GetErrorExample")
.WithOpenApi();

app.Logger.LogInformation("The app is ready to work ...");
app.Run();

record SimpleMessage(Guid Id, string Message, DateTime Now);
