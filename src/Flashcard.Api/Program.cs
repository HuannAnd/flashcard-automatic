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

app.MapGet("/api/v1/word/list", ([FromServices] IWordDbRepository repository) =>
{
    return repository.GetPagedList(0, 10).Items;

});


app.MapPost("api/v1/word/send", ([FromServices] IWordDbRepository repository, WordRequest request) =>
{
    repository.Add(new WordDocument
    {
        Date = DateTime.UtcNow,
        Word = request.Word
    });
});


app.Run();
