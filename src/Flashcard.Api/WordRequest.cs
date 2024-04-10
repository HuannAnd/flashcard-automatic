using Microsoft.AspNetCore.Mvc;

namespace Flashcard.Api;

public sealed class WordRequest
{
    public required string Word { get; set; }
    public DateTime? Date { get; set; }

    public WordRequest(string word, DateTime? date = null)
    {
        Word = word;
        Date = date;
    }
}
