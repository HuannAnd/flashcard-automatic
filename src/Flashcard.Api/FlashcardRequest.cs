using Microsoft.AspNetCore.Mvc;

namespace Flashcard.Api;

public sealed class FlashcardRequest
{
    [FromBody]
    public required IList<string> Words { get; set; }
    [FromBody]
    public DateTime? Date { get; set; }

    public FlashcardRequest(IList<string> words, DateTime? date = null)
    {
        Words = words;
        Date = date;
    }
}
