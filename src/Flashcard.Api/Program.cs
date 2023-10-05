using Flashcard.Api;

using Lab.Data.Mongo;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/flashcards", ([FromServices] IFlashcardDbRepository repository, FlashcardRequest request) =>
{
    repository.Add(new FlashcardDocument
    {
        Words = request.Words,
        Date = DateOnly.FromDateTime(request.Date ?? DateTime.Now)
    });

    return "Hello World!";
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/api/v1/flashcards", ([FromQuery] DateOnly date) =>
{
    return new string[]{
          "afoinagSOIUNDS",
        "Dragon � gay",
        "Hunter > Dragon",
        "GG ez"
        };
});

app.MapGet("/hello", () => 
{
    return new string[]{
          "afoinagSOIUNDS",
        "Dragon � gay",
        "Hunter > Dragon",
        "GG ez"
        };
});

app.Run();
